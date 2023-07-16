public class JwtLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public JwtLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Check if the request has an Authorization header
        if (context.Request.Headers.TryGetValue("Authorization", out var authHeader))
        {
            // Log the JWT
            Console.WriteLine($"JWT received: {authHeader}");
        }

        // Call the next middleware in the pipeline
        await _next(context);
    }
}
