namespace Lab.Common.AspCore.ExceptionHandling.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Lab.Common.AspCore.ExceptionHandling.Errors;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;

    public class AggregateExceptionHandler : ExceptionHandlerBase<AggregateException>
    {
        private readonly IServiceProvider _scopeProvider;

        public AggregateExceptionHandler(IServiceProvider scopeProvider)
        {
            _scopeProvider = scopeProvider;
        }

        /// <inheritdoc/>
        protected override BaseError HandleException(AggregateException exception)
        {
            using (var childScope = _scopeProvider.CreateScope())
            {
                var eventHandlers = childScope.ServiceProvider.GetService<IEnumerable<ExceptionHandler>>();

                List<BaseError> errors = new List<BaseError>();

                exception.Handle(ex =>
                {
                    var handledErrors = eventHandlers
                        .Where(handler => handler.CanHandle(ex))
                        .Select(handler => handler.Handle(ex))
                        .ToArray();

                    errors.AddRange(handledErrors);

                    return handledErrors.Any();
                });

                return CreateError(errors);
            }
        }

        private static BaseError CreateError(ICollection<BaseError> errors)
        {
            if (errors.Count == 1)
            {
                return errors.First();
            }

            return new AggregateError(StatusCodes.Status400BadRequest, errors.ToArray());
        }
    }
}
