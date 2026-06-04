using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ToDoOperations
{
    public static class DocDeleteToDoItem
    {
        [FunctionName("DocDeleteToDoItem")]
        public static async Task<IActionResult> Run(
           [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "items/{id}")] HttpRequest req,
           [CosmosDB(Connection = "CosmosDBConnection")] CosmosClient client,
           ILogger log, string id)
        {
            ArgumentNullException.ThrowIfNull(req);

            ArgumentNullException.ThrowIfNull(log);

            var container = client.GetContainer("ToDoItems", "Items");
            var document = await container.ReadItemAsync<ToDoItem>(id, new PartitionKey(id));
            if (document == null)
            {
                return new NotFoundResult();
            }
            await container.DeleteItemAsync<ToDoItem>(id, new PartitionKey(id));
            return new OkResult();
        }
    }
}
