using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureCosmosDB.DatabaseManagement
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string databaseId = "shop-database";
            string containerId = "books";
            string connectionString = "AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";

            // Connect to the Azure Cosmos Emulator running locally
            using (CosmosClient cosmosClient = new(connectionString))
            {
                // Create database
                Database database = await CreateDatabaseAsync(cosmosClient, databaseId);

                // Create collection
                Container container = await CreateContainerAsync(database, containerId);

                string bookId = Guid.NewGuid().ToString();
                Book book = new()
                {
                    Id = bookId,
                    Title = "Book " + bookId,
                    Price = 19.99m,
                    ISBN = "111-1111111111",
                    Authors = ["Author 1", "Author 2", "Author 3"],
                    PageCount = 500
                };

                // Create document
                await CreateBook(container, book);

                // Query Book by id
                await QueryBookAsync(container, book.ISBN);

                // Delete Book
                await DeleteBookAsync(container, book.Id, book.ISBN);

                // Delete database
                await DeleteDatabaseAsync(database, databaseId);
            }
        }

        private static async Task<Database> CreateDatabaseAsync(CosmosClient cosmosClient, string databaseId)
        {
            Database database = await cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
            Console.WriteLine("Created Database: {0}\n", database == null ? "no database" : database.Id);
            return database;
        }

        private static async Task<Container> CreateContainerAsync(Database database, string containerId)
        {
            // Create a new container
            Container container = await database.CreateContainerIfNotExistsAsync(containerId, "/ISBN");
            Console.WriteLine("Created Container: {0}\n", container.Id);
            return container;
        }

        private static async Task CreateBook(Container container, Book book)
        {
            ItemResponse<Book> bookResponse = await container.CreateItemAsync<Book>(book, new PartitionKey(book.ISBN));

            Console.WriteLine("Created item in database with id: {0} Operation consumed {1} RUs.\n", bookResponse.Resource.Id, bookResponse.RequestCharge);
        }

        private static async Task QueryBookAsync(Container container, string isbn)
        {
            var sqlQueryText = $"SELECT * FROM c WHERE c.ISBN = {isbn}";

            Console.WriteLine("Running query: {0}\n", sqlQueryText);

            QueryDefinition queryDefinition = new(sqlQueryText);
            using FeedIterator<Book> queryResultSetIterator = container.GetItemQueryIterator<Book>(queryDefinition);

            List<Book> books = [];

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<Book> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (Book book in currentResultSet)
                {
                    books.Add(book);
                    Console.WriteLine("\tRead {0}\n", book);
                }
            }
        }

        private static async Task DeleteBookAsync(Container container, string bookId, string isbn)
        {
            _ = await container.DeleteItemAsync<Book>(bookId, new PartitionKey(isbn));
            Console.WriteLine("Deleted Book [{0}]\n", bookId);
        }

        private static async Task DeleteDatabaseAsync(Database database, string databasseId)
        {
            _ = await database.DeleteAsync();

            Console.WriteLine("Deleted Database: {0}\n", databasseId);
        }
    }
}
