namespace EmployeeManagementAPI.Middlewares
{
    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;

        public JWTMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue("Source-Token", out var extractedToken))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Source Token is missing");
                return;
            }

            // Validate the token (example logic)
            if (extractedToken != "valid-source-token")
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid Source Token");
                return;
            }

            await _next(context);
        }
    }

    public static class SourceTokenMiddlewareExtensions
    {
        public static IApplicationBuilder UseSourceTokenMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JWTMiddleware>();
        }
    }
}
