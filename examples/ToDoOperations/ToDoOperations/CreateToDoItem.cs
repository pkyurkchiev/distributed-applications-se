using Newtonsoft.Json;

namespace ToDoOperations
{
    public class CreateToDoItem
    {
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
    }
}
