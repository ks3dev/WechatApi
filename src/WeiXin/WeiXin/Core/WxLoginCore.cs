using Infrastructure.Data;
using Infrastructure.WeiXin;
using Infrastructure.WeiXin.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Timers;
using WeiXin.Core.Interface;
using WeiXin.Domain;
using WeiXin.Domain.DTO;
using WeiXin.Domain.Interfaces;

namespace WeiXin.Core
{
    public class WxLoginCore
    {
        #region 获取登陆二维码

        /// <summary>
        /// 获取登陆二维码
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public static WxLoginQrCodeDto GetWxLoginQrCode()
        {
            var uuid = WeiXinHelper.GetWxUid();
            var url = WeiXinHelper.WxQrCodeUrl(uuid);
            return WxLoginQrCodeDto.GetQrCodeDto(uuid, url);
        }
        #endregion

        #region 登录
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <returns></returns>
        private static readonly object _lock = new object();
        public static int LoginSuccess(IWxUsersRepository repository,IWxFriendsRepository wxFriendsRepository,
            HttpResponse Response, IAuthCore authcore,string uuid,string tip,ref string url)
        {
            var status = WeiXinHelper.GetLoginStatus(uuid,tip, ref url);
            return status;
        }

        /// <summary>
        /// 登录初始化
        /// </summary>
        /// <returns></returns>
        public static void LoginInitialization(IWxUsersRepository repository,IWxFriendsRepository wxFriendsRepository, HttpResponse Response, IAuthCore authcore, string url)
        {
            var wxCookie = String.Empty;
            var keys = WeiXinHelper.GetUserKeys(url, ref wxCookie);

            //计算域名
            string hosts = url.IndexOf("wx.qq.com") > 0 ? "wx.qq.com" : "wx2.qq.com";

            //获取用户信息
            var userInfo = WeiXinHelper.GetWxUserInfo(hosts, keys);
            //创建用户
            var userModel = WxUsers.SetWxUserInfo(repository, userInfo.User, keys);
            //设置基础操作域名
            userModel.SetWxHosts(hosts);
            //同步好友
            var friends = WeiXinHelper.GetWxFriends(hosts, keys.skey, wxCookie);
            userModel.SyncFriends(wxFriendsRepository, friends);
            //创建状态通知
            WeiXinHelper.StartWxStatusNotify(hosts, wxCookie, keys, userModel.UserName);

            //心跳检测转到前端
            Thread thread = new Thread(m => HeartbeatWxStatus(Response, hosts, wxCookie, keys, userInfo.SyncKey));
            thread.IsBackground = true;
            thread.Start();

            //添加缓存
            AuthCore.SetUin(Response, userModel.Uin);
            authcore.SetUserName(userModel.Uin, userModel.UserName);
            authcore.SetUserHosts(userModel.Uin, hosts);
            authcore.SetUserKeys(userModel.Uin, keys);
            authcore.SetUserCookie(userModel.Uin, wxCookie);
            authcore.SetUserSyncKeys(userModel.Uin, userInfo.SyncKey);
        }

        #region 心跳检测微信状态
        /// <summary>
        /// 心跳检测微信状态
        /// </summary>
        public static void HeartbeatWxStatus(HttpResponse httpResponse, string wxHosts, string wxCookie, PassTicketXmlInfo keys, SyncKey syncKey)
        {
            var status = WeiXinHelper.GetWxStatus(wxHosts, wxCookie, keys, syncKey);
            if (status)
            {
                Thread.Sleep(20000);
                HeartbeatWxStatus(httpResponse, wxHosts, wxCookie, keys, syncKey);
                
            }
            else
            {
                throw new CustomerException(String.Format("当前登录状态消失 时间:", DateTime.Now),-2);
            }
        }

        #endregion
        #endregion
    }
}
