namespace SDKServer.Middleware;

public class NotFoundMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public NotFoundMiddleware(RequestDelegate next, ILogger<NotFoundMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext ctx)
    {
        await _next(ctx);

        if (ctx.Response.StatusCode is 404)
        {
            _logger.LogWarning("Unhandled: {query}", ctx.Request.Path);
        }
    }
}
