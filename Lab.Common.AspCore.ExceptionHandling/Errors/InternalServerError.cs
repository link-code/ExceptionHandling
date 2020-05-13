namespace Lab.Common.AspCore.ExceptionHandling.Errors
{
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;

    [JsonObject]
    public class InternalServerError : Error
    {
        [JsonConstructor]
        public InternalServerError(string message)
            : base(StatusCodes.Status500InternalServerError, message)
        {
        }
    }
}
