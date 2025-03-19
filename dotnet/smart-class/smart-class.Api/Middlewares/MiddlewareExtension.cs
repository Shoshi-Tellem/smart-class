namespace smart_class.Api.Middlewares
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder UseShabat(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ShabatMiddleWare>();
        }
    }
}
