namespace Lab.Common.AspCore.ExceptionHandling.Errors
{
    using Newtonsoft.Json;

    [JsonObject]
    public class ValidationErrorObject
    {
        [JsonConstructor]
        public ValidationErrorObject(
            string propertyName,
            params string[] messages)
        {
            PropertyName = propertyName;
            Messages = messages;
        }

        [JsonProperty(PropertyName = "propertyName")]
        public string PropertyName { get; }

        [JsonProperty(PropertyName = "messages")]
        public string[] Messages { get; }
    }
}
