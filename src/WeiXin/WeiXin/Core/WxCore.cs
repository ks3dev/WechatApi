using Infrastructure.Data;
using Infrastructure.Redis.Interface;
using Infrastructure.WeiXin;
using Infrastructure.WeiXin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeiXin.Core.Config;
using WeiXin.Domain.Interfaces;
using WeiXin.Domain.Param;

namespace WeiXin.Core
{
    public class WxCore
    {
        #region 设置备注名称

        /// <summary>
        /// 设置备注名称
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="wxHosts"></param>
        /// <param name="wxCookie"></param>
        /// <param name="keys"></param>
        /// <param name="userName"></param>
        /// <param name="remarkName"></param>
        /// <returns></returns>
        public static bool SetRemarkName(IWxFriendsRepository repository, string wxHosts, string wxCookie, PassTicketXmlInfo keys, string userName, string remarkName)
        {
            var status = WeiXinHelper.SetRemarkName(wxHosts, wxCookie, keys, userName, remarkName);
            if (status)
            {
                var users = repository.GetByName(keys.wxuin, userName);
                if (users == null)
                {
                    throw new CustomerException("本地同步失败", -1);
                }
                users.SetRemarkName(repository,remarkName);
            }
            return status;
        }
        #endregion

        #region 检测微信状态
        public static bool CheckStatus(HttpResponse httpResponse, string wxHosts, string wxCookie, PassTicketXmlInfo keys,SyncKey syncKey)
        {
            var status = WeiXinHelper.GetWxStatus(wxHosts, wxCookie, keys, syncKey);
            if (!status)
            {
                AuthCore.DeleteUin(httpResponse);
                throw new CustomerException("登录过期,请重新登录",-2);
            }
            return status;
        }
        #endregion

        #region 根据备注发送消息
        public static bool SendMsgByRemarkName(IWxFriendsRepository wxFriendsRepository,IWxUsersRepository wxUsersRepository,IRedisHelper redisHelper,
                                                IOptions<AppConfig> appConfig, WebHookParam webHook)
        {
            AuthCore authCore = new AuthCore(redisHelper);
            var username=authCore.GetUserName(appConfig.Value.UserUin);//当前登录用户名称
            var userHosts = authCore.GetUserHosts(appConfig.Value.UserUin);//当前登录用户操作域名
            var userCookies = authCore.GetUserCookie(appConfig.Value.UserUin);//当前登录用户Cookies
            var userKeys=authCore.GetUserKeys(appConfig.Value.UserUin);//当前登录用户秘钥


            var dbWxUser =wxUsersRepository.GetWxUsers(appConfig.Value.UserUin);//当前登录用户
            if (dbWxUser == null) throw new CustomerException("未找到登录信息", -2);
            var dbFriends=wxFriendsRepository.GetFriendByName(webHook.actor, dbWxUser.Uin);
            if (dbFriends == null) throw new CustomerException("未找到好友信息", -1);//好友信息

            return WeiXinHelper.SendMsg(userHosts, webHook.text, dbFriends.UserName, dbWxUser.UserName, userKeys, userCookies);
        }
        #endregion
    }
}
