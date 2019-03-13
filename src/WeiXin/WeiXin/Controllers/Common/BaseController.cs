using Infrastructure.Core.Api;
using Infrastructure.Core.DDD;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace WeiXin.Controllers
{
    public class BaseController : Controller
    {
        protected IServiceProvider ServiceProvider => Request.HttpContext.RequestServices;
        protected int Commit()
        {
            return ServiceProvider.GetService<IUnitOfWork>().Commit();
        }

        /// <summary>
        /// get请求返回json
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected new JsonResult Json(object data = null)
        {
            if (data == null)
            {
                data = new { };
            }
            var result = new ApiResult() { Data = data };
            return base.Json(result);
        }
    }
}
