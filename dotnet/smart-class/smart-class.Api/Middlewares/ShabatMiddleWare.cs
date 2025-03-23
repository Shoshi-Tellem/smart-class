namespace smart_class.Api.Middlewares
{
    public class ShabatMiddleWare(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            //if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
            //{
            //    context.Response.StatusCode = 400;
            //    context.Response.ContentType = "application/json";

            //    var result = new { error = "שגיאה: השירות אינו זמין בשבת." };
            //    await context.Response.WriteAsJsonAsync(result);
            //}
            //else
            //{
                await _next(context);
            //}
        }
    }
}