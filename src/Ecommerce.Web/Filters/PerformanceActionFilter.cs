using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace Ecommerce.Web.Filters;

public class PerformanceActionFilter : IActionFilter
{
    private Stopwatch? _stopwatch;
    private readonly ILogger<PerformanceActionFilter> _logger;

    public PerformanceActionFilter(ILogger<PerformanceActionFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        _stopwatch = Stopwatch.StartNew();
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        _stopwatch?.Stop();
        var elapsed = _stopwatch?.ElapsedMilliseconds ?? 0;

        if (elapsed > 1000)
        {
            _logger.LogWarning("Slow action {Action} took {Elapsed}ms",
                context.ActionDescriptor.DisplayName, elapsed);
        }

        context.HttpContext.Response.Headers.Append("X-Response-Time", $"{elapsed}ms");
    }
}
