using System.Text.Json.Serialization;

namespace MC.Infrastructure.Messaging
{
    public class ServiceResponseError : ServiceResponseBase
    {
        [JsonIgnore]
        public string? DeveloperError { get; set; }

        required public string Message { get; set; }

        public ServiceResponseError() : base(BusinessStatusCodeEnum.InternalServerError) { }
    }
}
