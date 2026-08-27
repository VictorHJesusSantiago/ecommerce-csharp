using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ecommerce.Web.Models;

public class ValidateAntiForgeryTokenAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.HttpContext.Request.Method == "POST")
        {
            var antiForgeryToken = context.HttpContext.Request.Form["__RequestVerificationToken"];
            if (string.IsNullOrEmpty(antiForgeryToken))
            {
                context.Result = new BadRequestObjectResult("Anti-forgery token is missing.");
            }
        }
        base.OnActionExecuting(context);
    }
}
