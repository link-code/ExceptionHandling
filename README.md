# ExceptionHandling

## Registration

``` csharp

    public void ConfigureServices(IServiceCollection services)
    {
        ...

        services.AddControllers(x =>
        {
            x.OutputFormatters.Insert(0, new ResponseFormatter());
        });
        services.AddExceptionHandling(Assembly.GetExecutingAssembly())  

        ...
    }

    public void Configure(IApplicationBuilder app)
    {
        ...
        app.UseRouting();
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        ...
    }

```

## Example of Exception Handler

### User not authorized exception handling
``` csharp

    public class NotAuthorizeExceptionHandler :     ExceptionHandlerBase<UserHasNoAccessException>
    {
        protected override BaseError HandleException(UserHasNoAccessException exception)
            => new Error(StatusCodes.Status403Forbidden, exception.Message);
    }

```

### For fluent validation exception handling

``` csharp

    public class ValidationExceptionHandler : ExceptionHandlerBase<ValidationException>
    {
        protected override BaseError HandleException(ValidationException exception) =>
            new ValidationError(
                StatusCodes.Status400BadRequest,
                exception.Errors.GroupBy(x => x.PropertyName)
                .Select(e => new ValidationErrorObject(e.Key, e.Select(x => x.ErrorMessage).ToArray()))
                .ToArray());

    }

```