using Newtonsoft.Json;

namespace MC.WebAPIServices.Messages
{
    public class ResponseMessage
    {
        [JsonProperty(PropertyName = "code")]
        public object Code { get; set; }

        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public object Body { get; set; }

        [JsonProperty(PropertyName = "error", NullValueHandling = NullValueHandling.Ignore)]
        public object Error { get; set; }
    }
}