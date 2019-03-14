using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure;
using Infrastructure.Redis.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WeiXin.Core;
using WeiXin.Core.Config;
using WeiXin.Domain.Interfaces;
using WeiXin.Domain.Param;

namespace WeiXin.Controllers
{
    public class OpenApiController : BaseController
    {
        private readonly IWxUsersRepository _wxUsersRepository;
        private readonly IWxFriendsRepository _wxFriendsRepository;
        private readonly IRedisHelper _redisHelper;
        private readonly IOptions<AppConfig> _appconfig;

        public OpenApiController(IWxUsersRepository wxUsersRepository, IWxFriendsRepository wxFriendsRepository,IRedisHelper redisHelper,IOptions<AppConfig> appconfig)
        {
            _wxUsersRepository = wxUsersRepository;
            _wxFriendsRepository = wxFriendsRepository;
            _redisHelper = redisHelper;
            _appconfig = appconfig;
        }
        [HttpPost]
        public JsonResult SendMsg(WebHookParam webHookParam)
        {
            var result=WxCore.SendMsgByRemarkName(_wxFriendsRepository, _wxUsersRepository, _redisHelper, _appconfig, webHookParam);
            return Json(result == true);
        }
    }
}