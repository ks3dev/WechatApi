using Infrastructure.Core.DDD;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeiXin.Core;
using WeiXin.Core.Interface;
using WeiXin.Domain;
using WeiXin.Domain.Interfaces;

namespace WeiXin.Controllers
{
    public class LoginController : BaseController
    {

        private readonly IAuthCore _authcore;
        private readonly IWxUsersRepository _repository;
        private readonly IWxFriendsRepository _wxFriendsRepository;
        public LoginController(IAuthCore authcore, IWxUsersRepository repository, IWxFriendsRepository wxFriendsRepository)
        {
            _authcore = authcore;
            _repository = repository;
            _wxFriendsRepository = wxFriendsRepository;
        }
        /// <summary>
        /// 登录二维码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult QrCode()
        {
            return View(WxLoginCore.GetWxLoginQrCode());
        }
        /// <summary>
        /// 验证登录
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult LoginSuccess(string uuid,string tip)
        {
            string url = string.Empty;
            var status=WxLoginCore.LoginSuccess(_repository, _wxFriendsRepository, HttpContext.Response, _authcore, uuid,tip,ref url);
            if (status == 200)
            {
                Commit();
            }
            return Json(new { status, url });
        }

        /// <summary>
        /// 信息初始化
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Initialization(string url)
        {
            WxLoginCore.LoginInitialization(_repository, _wxFriendsRepository, Response, _authcore, url);
            return Json();
        }
    }
}