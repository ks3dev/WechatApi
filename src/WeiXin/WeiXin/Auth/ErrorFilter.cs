using Infrastructure;
using Infrastructure.Core.Api;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WeiXin.Auth
{
    public class ErrorFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            int statusCode;
            string msg;
            var ex = context.Exception;
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
                LogHelper.Error("未捕获的异常", ex.Message);
            }
            context.Result= new JsonResult(new ApiResult
            {
                Data = msg,
                Code = statusCode
            });
        }
    }
}
