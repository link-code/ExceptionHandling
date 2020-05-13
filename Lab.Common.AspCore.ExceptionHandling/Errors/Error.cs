namespace Lab.Common.AspCore.ExceptionHandling.Errors
{
    using Newtonsoft.Json;

    [JsonObject]
    public class Error : BaseError
    {
        [JsonConstructor]
        public Error(int errorCode, string message)
           : base(errorCode)
        {
            Message = message;
        }

        [JsonProperty(PropertyName = "Message", Order = 1)]
        public string Message { get; }
    }
}
