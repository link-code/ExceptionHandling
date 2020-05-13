namespace Lab.Common.AspCore.ExceptionHandling.Errors
{
    using Newtonsoft.Json;

    [JsonObject]
    public class BaseError
    {
        [JsonConstructor]
        protected BaseError(int errorCode)
        {
            ErrorCode = errorCode;
        }

        [JsonIgnore]
        public int ErrorCode { get; set; }

        [JsonProperty("Activity_ID", Order = -1)]
        public string ActivityId { get; set; }
    }
}
