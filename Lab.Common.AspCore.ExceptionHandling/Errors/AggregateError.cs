namespace Lab.Common.AspCore.ExceptionHandling.Errors
{
    using Newtonsoft.Json;

    public class AggregateError : BaseError
    {
        [JsonConstructor]
        public AggregateError(int errorCode, BaseError[] errors)
            : base(errorCode)
        {
            Errors = errors;
        }

        [JsonProperty(PropertyName = "Errors", Order = 1)]
        public BaseError[] Errors { get; }
    }
}
