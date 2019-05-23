using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Linq;

namespace AzureCosmosDB.DatabaseManagement
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string databaseId = "shop-database";
            string collectionId = "products";
            Uri databaseUri = new Uri("https://localhost:8081");
            string primaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";

            // Connect to the Azure Cosmos Emulator running locally
            using (DocumentClient documentClient = new DocumentClient(databaseUri, primaryKey))
            {
                string databaseLink = await CreateDatabase(documentClient, databaseId);

                string collectionLink = await CreateCollection(documentClient, databaseLink, collectionId);

                await CreateBookItem(documentClient, collectionLink, 1);

                await DeleteDatabase(documentClient, databaseId);
            }

        }
        private static async Task<string> CreateDatabase(DocumentClient documentClient, string databaseId)
        {
            Database database = documentClient.CreateDatabaseQuery().Where(db => db.Id == databaseId).AsEnumerable().FirstOrDefault();
            Console.WriteLine("1. Query for a database returned: {0}", database == null ? "no results" : database.Id);

            if (database == null)
            {
                database = await documentClient.CreateDatabaseAsync(new Database { Id = databaseId });
                Console.WriteLine("\n2. Created Database: id - {0} and selfLink - {1}", database.Id, database.SelfLink);
            }

            return database.SelfLink;
        }

        private static async Task<string> CreateCollection(DocumentClient documentClient, string databaseLink, string collectionName)
        {
            DocumentCollection documentCollection = await documentClient.CreateDocumentCollectionIfNotExistsAsync(databaseLink, 
                new DocumentCollection { Id = collectionName },
                new RequestOptions { OfferThroughput = 10000 });

            Console.WriteLine("\n3. Created Database Collention: id - {0}", documentCollection.Id);

            return documentCollection.SelfLink;
        }

        private static async Task CreateBookItem(DocumentClient documentClient, string selftLink, int sampleBookId)
        {
            var book = new Book
            {
                Id = sampleBookId,
                Title = "Book " + sampleBookId,
                Price = 19.99m,
                ISBN = "111-1111111111",
                Authors = new List<string> { "Author 1", "Author 2", "Author 3" },
                PageCount = 500
            };

            await documentClient.CreateDocumentAsync(selftLink, book);
            Console.WriteLine("\n4 Create book item");
        }

        private static async Task DeleteDatabase(DocumentClient documentClient, string databaseId)
        {
            await documentClient.DeleteDatabaseAsync(UriFactory.CreateDatabaseUri(databaseId));

            Console.WriteLine("\n5. Database {0} deleted.", databaseId);
        }
    }
}
