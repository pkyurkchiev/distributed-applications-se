using Newtonsoft.Json;

namespace ToDoOperations
{
    public class UpdateToDoItem
    {
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "isCompleted")]
        public bool IsCompleted { get; set; }
    }
}
