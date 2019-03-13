using Infrastructure.WeiXin.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeiXin.Core.Interface
{
    public interface IAuthCore
    {
        void SetUserName(string wxuin, string username);
        string GetUserName(string wxuin);
        void SetUserHosts(string wxuin, string hosts);
        string GetUserHosts(string wxuin);
        void SetUserKeys(string wxuin, PassTicketXmlInfo keys);
        PassTicketXmlInfo GetUserKeys(string wxuin);
        void SetUserCookie(string wxuin, string cookie);
        string GetUserCookie(string wxuin);
        void SetUserSyncKeys(string wxuin, SyncKey SyncKeys);
        SyncKey GetUserSyncKeys(string wxuin);
    }
}
