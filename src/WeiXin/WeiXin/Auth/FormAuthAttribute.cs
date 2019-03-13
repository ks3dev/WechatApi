using Infrastructure.Core.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WeiXin.Auth
{
    /// <summary>
    /// 表单验证过滤器
    /// </summary>
    public class FormAuthAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.ModelState.IsValid)
            {
                ApiResult result = new ApiResult { Code = -1 };
                foreach (var item in filterContext.ModelState.Values)
                {
                    foreach (var error in item.Errors)
                    {
                        result.Data=error.ErrorMessage;
                    }
                }
                filterContext.Result = new JsonResult(result);
            }
        }
    }
}
