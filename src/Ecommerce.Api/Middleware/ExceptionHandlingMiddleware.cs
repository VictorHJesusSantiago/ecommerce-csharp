using System.Text.Json;

namespace Ecommerce.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var (statusCode, message) = exception switch
        {
            KeyNotFoundException ex => (404, ex.Message),
            UnauthorizedAccessException ex => (401, ex.Message),
            ForbiddenAccessException ex => (403, ex.Message),
            ArgumentException ex => (400, ex.Message),
            InvalidOperationException ex => (400, ex.Message),
            TimeoutException ex => (408, "Request timeout"),
            _ => (500, "An internal server error occurred.")
        };

        var response = new Ecommerce.Application.Wrappers.ApiResponse
        {
            Success = false,
            Message = message,
            StatusCode = statusCode
        };

        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsync(JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        }));
    }
}

public class ForbiddenAccessException : Exception
{
    public ForbiddenAccessException() : base("You do not have permission to perform this action.") { }
    public ForbiddenAccessException(string message) : base(message) { }
}

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var correlationId = context.Request.Headers["X-Correlation-Id"].FirstOrDefault() ?? Guid.NewGuid().ToString();
        context.Items["CorrelationId"] = correlationId;
        context.Response.Headers.Append("X-Correlation-Id", correlationId);

        var sw = System.Diagnostics.Stopwatch.StartNew();
        _logger.LogInformation("HTTP {Method} {Path} started [CorrelationId: {CorrelationId}]",
            context.Request.Method, context.Request.Path, correlationId);

        await _next(context);

        sw.Stop();
        _logger.LogInformation("HTTP {Method} {Path} completed {StatusCode} in {ElapsedMs}ms [CorrelationId: {CorrelationId}]",
            context.Request.Method, context.Request.Path, context.Response.StatusCode, sw.ElapsedMilliseconds, correlationId);
    }
}

public class CorrelationIdMiddleware
{
    private readonly RequestDelegate _next;

    public CorrelationIdMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        var correlationId = context.Request.Headers["X-Correlation-Id"].FirstOrDefault() ?? Guid.NewGuid().ToString();
        context.Items["CorrelationId"] = correlationId;
        context.Response.Headers.Append("X-Correlation-Id", correlationId);
        await _next(context);
    }
}

public class RateLimitingMiddleware
{
    private readonly RequestDelegate _next;
    private static readonly System.Collections.Concurrent.ConcurrentDictionary<string, DateTime> _requests = new();

    public RateLimitingMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        var now = DateTime.UtcNow;
        if (_requests.TryGetValue(ip, out var lastRequest) && (now - lastRequest).TotalSeconds < 1)
        {
            context.Response.StatusCode = 429;
            await context.Response.WriteAsync("{\"message\":\"Rate limit exceeded. Please try again later.\"}");
            return;
        }
        _requests[ip] = now;
        await _next(context);
    }
}
