namespace Lab.Common.AspCore.Extensions.Responses
{
    using Newtonsoft.Json;

    [JsonObject]
    public class SuccessResponse<T> : BaseResponse
    {
        [JsonConstructor]
        public SuccessResponse(T result)
        {
            IsSuccess = true;
            Result = result;
        }

        [JsonProperty("result")]
        public T Result { get; set; }
    }
}
