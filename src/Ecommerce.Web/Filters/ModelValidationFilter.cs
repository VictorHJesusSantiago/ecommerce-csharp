using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ecommerce.Web.Filters;

public class ModelValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState
                .Where(e => e.Value?.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                );

            if (context.HttpContext.Request.Headers.Accept.ToString().Contains("application/json"))
            {
                context.Result = new BadRequestObjectResult(new { Errors = errors });
            }
            else
            {
                context.Result = new ViewResult { StatusCode = 400 };
            }
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
