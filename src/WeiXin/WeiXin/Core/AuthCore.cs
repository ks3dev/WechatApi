using Infrastructure;
using Infrastructure.Redis.Interface;
using Infrastructure.WeiXin.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeiXin.Core.Interface;

namespace WeiXin.Core
{
    /// <summary>
    /// 认证保存
    /// </summary>
    public class AuthCore: IAuthCore
    {
        private readonly IRedisHelper _redisHelper;
        public AuthCore(IRedisHelper redisHelper)
        {
            _redisHelper = redisHelper;
        }
        #region Uin存储

        private const string _userName_cookie_key_ = "_WeiXin_Uin_";
        /// <summary>
        /// 存储Uin
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="UserName"></param>
        public static void SetUin(HttpResponse httpResponse,string wxuin)
        {
            var md5CookieKey = EncryptionHelper.MD5Encrypt(_userName_cookie_key_, Encoding.ASCII);
            var desUserName=EncryptionHelper.DESEncrypt(wxuin, _userName_cookie_key_);
            httpResponse.Cookies.Append(md5CookieKey, desUserName);
        }
        /// <summary>
        /// 获取Uin
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <returns></returns>
        public static string GetUin(HttpRequest httpRequest)
        {
            var desUserName = String.Empty;
            var md5CookieKey = EncryptionHelper.MD5Encrypt(_userName_cookie_key_, Encoding.ASCII);
            httpRequest.Cookies.TryGetValue(md5CookieKey,out desUserName);
            if (String.IsNullOrEmpty(desUserName)) return null;
            var result = EncryptionHelper.DESDecrypt(desUserName, _userName_cookie_key_);
            return result;
        }
        /// <summary>
        /// 删除Uin
        /// </summary>
        public static void DeleteUin(HttpResponse httpResponse)
        {
            var md5CookieKey = EncryptionHelper.MD5Encrypt(_userName_cookie_key_, Encoding.ASCII);
            httpResponse.Cookies.Delete(md5CookieKey);
        }
        #endregion

        #region 存储用户名
        //用户redis key
        private string userRedisKey(string userName) => "_user_redis_key_" + userName;
        private const string _username_redis_key_ = "_WeiXin_UserName_";
        /// <summary>
        /// 存储用户名
        /// </summary>
        /// <param name="redisHelper"></param>
        /// <param name="userName"></param>
        /// <param name="keys"></param>
        public void SetUserName(string wxuin, string userName)
        {
            var tablename = userRedisKey(wxuin);
            _redisHelper.SetHash(tablename, _username_redis_key_, userName);
        }
        /// <summary>
        /// 获取用户名
        /// </summary>
        /// <returns></returns>
        public string GetUserName(string wxuin)
        {
            var tablename = userRedisKey(wxuin);
            return _redisHelper.GetHash(tablename, _username_redis_key_);
        }
        #endregion

        #region 存储用户操作域名
        private const string _hosts_redis_key_ = "_WeiXin_Hosts_";
        /// <summary>
        /// 存储用户操作域名
        /// </summary>
        /// <param name="redisHelper"></param>
        /// <param name="userName"></param>
        /// <param name="hosts"></param>
        public void SetUserHosts(string wxuin, string hosts)
        {
            var tablename = userRedisKey(wxuin);
            _redisHelper.SetHash(tablename, _hosts_redis_key_, hosts);
        }
        /// <summary>
        /// 获取用户操作域名
        /// </summary>
        /// <param name="redisHelper"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string GetUserHosts(string wxuin)
        {
            var tablename = userRedisKey(wxuin);
            return _redisHelper.GetHash(tablename, _hosts_redis_key_);
        }
        #endregion

        #region 存储用户操作秘钥

        private const string _keys_redis_key_ = "_WeiXin_Keys_";
        /// <summary>
        /// 存储用户操作秘钥
        /// </summary>
        /// <param name="redisHelper"></param>
        /// <param name="userName"></param>
        /// <param name="keys"></param>
        public void SetUserKeys(string wxuin, PassTicketXmlInfo keys)
        {
            var tablename = userRedisKey(wxuin);
            string keysJson=(JsonConvert.SerializeObject(keys));
            _redisHelper.SetHash(tablename, _keys_redis_key_, keysJson);
        }
        /// <summary>
        /// 获取用户操作秘钥
        /// </summary>
        /// <returns></returns>
        public PassTicketXmlInfo GetUserKeys(string wxuin)
        {
            var tablename = userRedisKey(wxuin);
            string keysJson = _redisHelper.GetHash(tablename, _keys_redis_key_);
            return JsonConvert.DeserializeObject<PassTicketXmlInfo>(keysJson);
        }
        #endregion

        #region 存储用户微信Cookie标识

        private const string _cookie_redis_key_ = "_WeiXin_Cookie_";
        /// <summary>
        /// 存储用户微信Cookie标识
        /// </summary>
        /// <param name="redisHelper"></param>
        /// <param name="userName"></param>
        /// <param name="cookie"></param>
        public void SetUserCookie(string wxuin, string cookie )
        {
            var tablename = userRedisKey(wxuin);
            _redisHelper.SetHash(tablename, _cookie_redis_key_, cookie);
        }
        /// <summary>
        /// 获取用户Cookie标识
        /// </summary>
        /// <param name="redisHelper"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string GetUserCookie(string wxuin)
        {
            var tablename = userRedisKey(wxuin);
            return _redisHelper.GetHash(tablename, _cookie_redis_key_);
        }
        #endregion

        #region 存储用户微信SyncKeys标识

        private const string _synckeys_redis_key_ = "_WeiXin_SyncKeys_";
        /// <summary>
        /// 存储用户微信Cookie标识
        /// </summary>
        /// <param name="redisHelper"></param>
        /// <param name="userName"></param>
        /// <param name="cookie"></param>
        public void SetUserSyncKeys(string wxuin, SyncKey SyncKeys)
        {
            var tablename = userRedisKey(wxuin);
            string keysJson = (JsonConvert.SerializeObject(SyncKeys));
            _redisHelper.SetHash(tablename, _synckeys_redis_key_, keysJson);
        }
        /// <summary>
        /// 获取用户Cookie标识
        /// </summary>
        /// <param name="redisHelper"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public SyncKey GetUserSyncKeys(string wxuin)
        {
            var tablename = userRedisKey(wxuin);
            string keysJson = _redisHelper.GetHash(tablename, _synckeys_redis_key_);
            return JsonConvert.DeserializeObject<SyncKey>(keysJson);
        }
        #endregion
    }
}
