using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ToDoOperations
{
    public static class DocUpdateToDoItem
    {
        [FunctionName("DocUpdateToDoItem")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "items/{id}")] HttpRequest req,
            [CosmosDB(Connection = "CosmosDBConnection")] CosmosClient client,
            ILogger log, string id)
        {
            ArgumentNullException.ThrowIfNull(log);

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var updated = JsonConvert.DeserializeObject<UpdateToDoItem>(requestBody);
            var container = client.GetContainer("ToDoItems", "Items");
            var document = await container.ReadItemAsync<ToDoItem>(id, new PartitionKey(id));
            if (document == null)
            {
                return new NotFoundResult();
            }

            document.Resource.IsCompleted = updated.IsCompleted;
            if (!string.IsNullOrEmpty(updated.Description))
            {
                document.Resource.Description = updated.Description;
            }

            await container.ReplaceItemAsync(document, id);

            // an easier way to deserialize a Document
            ToDoItem todo2 = (dynamic)document;

            return new OkObjectResult(todo2);
        }
    }
}
