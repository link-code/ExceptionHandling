namespace Lab.Common.AspCore.ExceptionHandling.Errors
{
    using Newtonsoft.Json;

    [JsonObject]
    public class ValidationError : BaseError
    {
        [JsonConstructor]
        public ValidationError(int errorCode,
            ValidationErrorObject[] errors)
            : base(errorCode)
        {
            Errors = errors;
        }

        [JsonProperty(PropertyName = "errors")]
        public ValidationErrorObject[] Errors { get; }
    }
}
