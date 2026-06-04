using System;
using Newtonsoft.Json;

namespace ToDoOperations
{
    public class ToDoItem
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; } = Guid.NewGuid().ToString("n");
        [JsonProperty(PropertyName = "createdOn")]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "isCompleted")]
        public bool IsCompleted { get; set; }
    }
}
