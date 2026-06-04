using Newtonsoft.Json;
using System.Collections.Generic;

namespace AzureCosmosDB.DatabaseManagement
{
    public class Book
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string ISBN { get; set; }
        public List<string> Authors  { get; set; }
        public int PageCount { get; set; }
    }
}
