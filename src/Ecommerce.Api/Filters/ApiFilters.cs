using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Ecommerce.Application.Wrappers;

namespace Ecommerce.Api.Filters;

public class ValidationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState
                .Where(e => e.Value?.Errors.Count > 0)
                .SelectMany(e => e.Value!.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            var response = new ApiResponse<string>
            {
                Success = false,
                Message = "Validation failed",
                Errors = errors,
                StatusCode = 400
            };

            context.Result = new BadRequestObjectResult(response);
            return;
        }

        await next();
    }
}

public class ApiExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ApiExceptionFilter> _logger;

    public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger) => _logger = logger;

    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, "Unhandled exception in controller action");

        var response = new ApiResponse
        {
            Success = false,
            Message = "An error occurred while processing your request.",
            StatusCode = 500
        };

        context.Result = new ObjectResult(response)
        {
            StatusCode = 500
        };

        context.ExceptionHandled = true;
    }
}

public class ApiKeyAuthFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue("X-Api-Key", out var apiKey))
        {
            context.Result = new UnauthorizedObjectResult(ApiResponse.FailResponse("API key is missing.", 401));
            return;
        }

        if (apiKey != "test-api-key")
        {
            context.Result = new UnauthorizedObjectResult(ApiResponse.FailResponse("Invalid API key.", 401));
            return;
        }

        await next();
    }
}

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
public class ValidatePaginationAttribute : Attribute, IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ActionArguments.TryGetValue("page", out var pageObj) && pageObj is int page && page < 1)
            context.ModelState.AddModelError("Page", "Page must be greater than 0.");

        if (context.ActionArguments.TryGetValue("pageSize", out var pageSizeObj) && pageSizeObj is int pageSize && (pageSize < 1 || pageSize > 100))
            context.ModelState.AddModelError("PageSize", "PageSize must be between 1 and 100.");
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
