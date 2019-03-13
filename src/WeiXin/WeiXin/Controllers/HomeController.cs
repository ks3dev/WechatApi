using Infrastructure.Core.DDD;
using Infrastructure.WeiXin;
using Microsoft.AspNetCore.Mvc;
using WeiXin.Core;
using WeiXin.Domain;
using WeiXin.Domain.DTO;
using WeiXin.Domain.Interfaces;
using WeiXin.Domain.Param;

namespace WeiXin.Controllers
{
    public class HomeController : BaseUserController
    {
        private readonly IWxFriendsRepository _wxFriendsRepository;
        private readonly IQuery<WxFriends> _query;
        public HomeController(IWxFriendsRepository wxFriendsRepository, IQuery<WxFriends> query)
        {
            _wxFriendsRepository = wxFriendsRepository;
            _query = query;
        }

        #region 好友列表

        /// <summary>
        /// 好友列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            var result=WxFriendsDto.GetFriends(_query,Uin);
            return View(result);
        }
        #endregion

        #region 发消息

        /// <summary>
        /// 发消息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="toUserName"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SendMsg(SendMsgParam sendMsg)
        {
            var status = WeiXinHelper.SendMsg(UserHosts, sendMsg.Msg, sendMsg.FriendsName, UserName,UserKeys,WxCookie);
            return Json(status);
        }
        #endregion

        #region 设置备注

        /// <summary>
        /// 设置备注
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SetRemarkName(RemarkNameParam remarkNameParam)
        {
            var status = WxCore.SetRemarkName(_wxFriendsRepository, UserHosts, WxCookie, UserKeys, remarkNameParam.FriendsName, remarkNameParam.RemarkName);
            if (status) status = Commit() == 1;
            return Json(status);
        }
        #endregion

        #region 绑定手机号

        /// <summary>
        /// 绑定手机号
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult BindTelephone(BindTelephoneParam bindTelephone)
        {
            var friends = _wxFriendsRepository.GetByName(Uin, bindTelephone.FriendsName);
            friends.BindTelephone(_wxFriendsRepository, bindTelephone.Telephone);
            var status = Commit() >= 1;
            return Json(status);
        }
        #endregion

        #region 检测微信状态

        /// <summary>
        /// 检测微信状态
        /// </summary>
        [HttpPost]
        public JsonResult CheckStatus()
        {
            var status =WxCore.CheckStatus(HttpContext.Response,UserHosts,WxCookie,UserKeys,UserSyncKeys);
            return Json(status);
        }
        #endregion
    }
}
