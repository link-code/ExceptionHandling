namespace Lab.Common.AspCore.Extensions.Responses
{
    using Newtonsoft.Json;

    [JsonObject]
    public class BaseResponse
    {
        [JsonProperty(PropertyName = "isSuccess")]
        public bool IsSuccess { get; set; }
    }
}
