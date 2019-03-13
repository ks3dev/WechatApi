using Infrastructure.WeiXin.Models;
using Microsoft.Extensions.DependencyInjection;
using WeiXin.Auth;
using WeiXin.Core;
using WeiXin.Core.Interface;

namespace WeiXin.Controllers
{
    [WeiXinAuth]
    public class BaseUserController : BaseController
    {
        #region 账户信息

        /// <summary>
        /// 账号
        /// </summary>
        protected string Uin
        {
            get
            {
                return AuthCore.GetUin(HttpContext.Request);
            }
        }
        protected string UserName
        {
            get
            {
                return ServiceProvider.GetService<IAuthCore>().GetUserName(Uin);
            }
        }
        /// <summary>
        /// 用户操作域名
        /// </summary>
        protected string UserHosts
        {
            get
            {
                return ServiceProvider.GetService<IAuthCore>().GetUserHosts(Uin);
            }
        }

        /// <summary>
        /// 账户信息
        /// </summary>
        /// <returns></returns>
        protected PassTicketXmlInfo UserKeys
        {
            get
            {
                return ServiceProvider.GetService<IAuthCore>().GetUserKeys(Uin);
            }
        }

        /// <summary>
        /// 账户信息
        /// </summary>
        /// <returns></returns>
        protected SyncKey UserSyncKeys
        {
            get
            {
                return ServiceProvider.GetService<IAuthCore>().GetUserSyncKeys(Uin);
            }
        }
        /// <summary>
        /// 微信Cookie
        /// </summary>
        protected string WxCookie
        {
            get
            {
                return ServiceProvider.GetService<IAuthCore>().GetUserCookie(Uin);
            }
        }
        #endregion
    }
}
