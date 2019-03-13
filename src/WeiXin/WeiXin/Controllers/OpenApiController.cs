using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WeiXin.Controllers
{
    public class OpenApiController : Controller
    {
        [HttpPost]
        public JsonResult SendMsg([FromBody]object webHookParam)
        {
            var paramjson = JsonConvert.SerializeObject(webHookParam);
            LogHelper.Error("接收Param", paramjson);
            return Json(true);
        }
    }
}