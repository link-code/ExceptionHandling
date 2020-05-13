namespace Lab.Common.AspCore.Extensions.Responses
{
    using Lab.Common.AspCore.ExceptionHandling.Errors;
    using Newtonsoft.Json;

    [JsonObject]
    public class ErrorResponse<T> : BaseResponse
        where T : BaseError
    {
        [JsonConstructor]
        public ErrorResponse(T error)
        {
            Error = error;
        }

        [JsonProperty(PropertyName = "error")]
        public T Error { get; set; }
    }
}
