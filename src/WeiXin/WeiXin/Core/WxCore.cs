using Infrastructure.Data;
using Infrastructure.WeiXin;
using Infrastructure.WeiXin.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeiXin.Domain.Interfaces;

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
    }
}
