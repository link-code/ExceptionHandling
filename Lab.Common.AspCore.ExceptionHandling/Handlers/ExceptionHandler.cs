namespace Lab.Common.AspCore.ExceptionHandling.Handlers
{
    using System;
    using Lab.Common.AspCore.ExceptionHandling.Errors;

    public interface ExceptionHandler
    {
        bool CanHandle(Exception exception);

        BaseError Handle(Exception exception);
    }
}
