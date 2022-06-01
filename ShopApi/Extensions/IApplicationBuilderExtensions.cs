using ShopApi.Middlewares;

namespace ShopApi.Extensions;

public static class IApplicationBuilderExtensions
{
    public static void UseGlobalExceptionHandler(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<GlobalExceptionHandlerMiddleware>();
    }
}
