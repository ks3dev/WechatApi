using Infrastructure.Core.Api;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                int statusCode;
                string msg;
                if (ex is CustomerException)
                {
                    statusCode = -1;
                    msg = ex.Message;
                    LogHelper.Error("自定义异常", ex);
                }
                else
                {
                    statusCode = -1;
                    msg = "系统繁忙，请稍后再试";
                    LogHelper.Error("未捕获的异常", ex);
                }

                await HandleExceptionAsync(context, statusCode, msg);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, int statusCode, string msg)
        {
            var data = new ApiErrResult() { Code = statusCode, Msg = msg };

            var result = JsonConvert.SerializeObject(data);
            if (context.Response.HasStarted)
            {
                return context.Response.WriteAsync(result);
            }
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json;charset=utf-8";
            return context.Response.WriteAsync(result);
        }
    }
    public static class ErrorHandlingExtensions
    {
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
