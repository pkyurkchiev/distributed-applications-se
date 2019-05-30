using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Documents.Client;
using System.Linq;

namespace ToDoOperations
{
    public static class DocDeleteToDoItem
    {
        [FunctionName("DocDeleteToDoItem")]
        public static async Task<IActionResult> Run(
           [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "items/{id}")]HttpRequest req,
           [CosmosDB(ConnectionStringSetting = "CosmosDBConnection")] DocumentClient client,
           ILogger log, string id)
        {
            Uri collectionUri = UriFactory.CreateDocumentCollectionUri("ToDoItems", "Items");
            var document = client.CreateDocumentQuery(collectionUri).Where(t => t.Id == id)
                    .AsEnumerable().FirstOrDefault();
            if (document == null)
            {
                return new NotFoundResult();
            }
            await client.DeleteDocumentAsync(document.SelfLink, options: new RequestOptions { PartitionKey = new Microsoft.Azure.Documents.PartitionKey(id) });
            return new OkResult();
        }
    }
}
