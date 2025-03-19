namespace smart_class.Api.Middlewares
{
    public class ShabatMiddleWare(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;
        public async Task InvokeAsync(HttpContext context)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
                context.Response.StatusCode = 400;
            else
                await _next(context);
        }
    }
}
