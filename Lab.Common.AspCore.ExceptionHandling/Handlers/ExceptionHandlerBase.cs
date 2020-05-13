namespace Lab.Common.AspCore.ExceptionHandling.Handlers
{
    using System;
    using Lab.Common.AspCore.ExceptionHandling.Errors;

    public abstract class ExceptionHandlerBase<TException> : ExceptionHandler
         where TException : Exception
    {
        /// <inheritdoc/>
        public virtual bool CanHandle(Exception exception)
        {
            return exception is TException;
        }

        /// <inheritdoc/>
        public virtual BaseError Handle(Exception exception)
        {
            if (!CanHandle(exception))
            {
                throw new NotSupportedException($"Cannot handle exception of type {exception.GetType()}");
            }

            BaseError error = HandleException(exception as TException);

            return error;
        }

        protected abstract BaseError HandleException(TException exception);
    }
}
