namespace Lab.Common.AspCore.Extensions.Formatters
{
    using System.Threading.Tasks;
    using Lab.Common.AspCore.ExceptionHandling.Errors;
    using Lab.Common.AspCore.Extensions.Responses;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class ResponseFormatter : OutputFormatter
    {
        public ResponseFormatter()
        {
            SupportedMediaTypes.Add("application/json");
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
        {
            BaseResponse response = null;

            switch (context.Object)
            {
                case BaseError error:
                    response = new ErrorResponse<BaseError>(error);
                    break;
                default:
                    response = new SuccessResponse<object>(context.Object);
                    break;
            }

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            return context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(response, jsonSerializerSettings));
        }
    }
}
