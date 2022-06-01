using ShopApi.CustomExceptions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ShopApi.Middlewares
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate requestDelegate;

        public GlobalExceptionHandlerMiddleware(RequestDelegate requestDelegate)
        {
            this.requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await requestDelegate(context);
            }
            catch (Exception ex)
            {
                if (ex is EntryNotFoundException entryNotFoundExc)
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsJsonAsync(new { Message = entryNotFoundExc.Message });
                }
                else
                {
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsJsonAsync(new { Message = ex.Message });
                }
            }
        }
    }
}
