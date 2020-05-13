namespace Lab.Common.AspCore.ExceptionHandling.Middlewares
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Lab.Common.AspCore.ExceptionHandling.Errors;
    using Lab.Common.AspCore.ExceptionHandling.Handlers;
    using Lab.Common.AspCore.Extensions.Responses;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    public class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IEnumerable<ExceptionHandler> _exceptionHandlers;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(
            IHostingEnvironment hostingEnvironment,
            IEnumerable<ExceptionHandler> exceptionHandlers,
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _hostingEnvironment = hostingEnvironment;
            _exceptionHandlers = exceptionHandlers;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task CreateResponseAsync(HttpContext context, BaseError error)
        {
            error.ActivityId = context.TraceIdentifier;
            var response = JsonConvert.SerializeObject(new ErrorResponse<BaseError>(error));
            context.Response.StatusCode = error.ErrorCode;
            await context.Response.WriteAsync(response);
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            try
            {
                var exceptionHandler = _exceptionHandlers
                    .Where(handler => handler.CanHandle(ex))
                    .Single(handler => handler.CanHandle(ex));

                await CreateResponseAsync(context, exceptionHandler.Handle(ex));
            }
            catch (Exception)
            {
                await CreateResponseAsync(context, CreateInternalServerError(ex));
            }
        }

        private InternalServerError CreateInternalServerError(Exception ex)
        {
            return new InternalServerError(ex.Message);
        }
    }
}
