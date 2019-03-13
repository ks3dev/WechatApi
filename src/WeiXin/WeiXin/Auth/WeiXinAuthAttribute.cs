using Infrastructure;
using Infrastructure.Core.Api;
using Infrastructure.Redis.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeiXin.Core;

namespace WeiXin.Auth
{
    public class WeiXinAuthAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                var uin = AuthCore.GetUin(context.HttpContext.Request);
                if (String.IsNullOrEmpty(uin))
                {
                    bool isAjax = false;
                    if (context.HttpContext.Request.Headers.ContainsKey("x-requested-with"))
                    {
                        isAjax = context.HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest";
                    }

                    if (!isAjax)
                    {
                        RedirectToActionResult content = new RedirectToActionResult("QrCode", "Login", null);
                        context.Result = content;
                    }
                    else
                    {
                        context.Result =
                            new JsonResult(new ApiResult()
                            {
                                Code = -2,
                                Data = "登录过期，请重新登录"
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                RedirectToActionResult content = new RedirectToActionResult("QrCode", "Login", null);
                context.Result = content;
                LogHelper.Error("登录验证抛出异常",ex.Message);
            }
        }
    }
}
