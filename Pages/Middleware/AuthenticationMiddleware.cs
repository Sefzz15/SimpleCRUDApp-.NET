using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace backend.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path;

            if (path.StartsWithSegments("/Login/Login", StringComparison.OrdinalIgnoreCase) ||
                path.StartsWithSegments("/Users/Create", StringComparison.OrdinalIgnoreCase))
            {
                await _next(context);
                return;
            }

            if (string.IsNullOrEmpty(context.Session.GetString("Username")))
            {
                context.Response.Redirect("/Login/Login");
                return;
            }

            await _next(context);
        }
    }
}
