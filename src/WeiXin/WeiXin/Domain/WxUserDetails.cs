using Infrastructure.Core.DDD;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace WeiXin.Domain
{
    public partial class WxUserDetails:Entity
    {
        /// <summary>
        /// 微信操作访问域名
        /// </summary>
        [MaxLength(10)]
        public string Hosts { get; set; }

        /// <summary>
        /// 微信相关IDJson
        /// </summary>
        [MaxLength(-1)]
        public string WxKeysJson { get; set; } = "{}";

        /// <summary>
        /// 微信相关ID
        /// </summary>
        public virtual WxKey WxKeys
        {
            get => JsonConvert.DeserializeObject<WxKey>(WxKeysJson);
            set => WxKeysJson = JsonConvert.SerializeObject(value);
        }

        public string UserUin { get; set; } 
        /// <summary>
        /// 用户主信息
        /// </summary>
        public WxUsers Users { get; set; } = null;

    }
    public class WxKey
    {
        #region XML
        /// <summary>
        /// Ret
        /// </summary>
        public int Ret { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Skey
        /// </summary>
        public string Skey { get; set; }

        /// <summary>
        /// WxSid
        /// </summary>
        public string WxSid { get; set; }

        /// <summary>
        /// WxUin
        /// </summary>
        public string WxUin { get; set; }

        /// <summary>
        /// PassTicket
        /// </summary>
        public string PassTicket { get; set; }

        /// <summary>
        /// Isgrayscale
        /// </summary>
        public string Isgrayscale { get; set; }
        #endregion

        /// <summary>
        /// Cookie
        /// </summary>
        public CookieCollection Cookie { get; set; }
        /// <summary>
        /// 账户标识
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// UUid
        /// </summary>
        public string UUid { get; set; }
    }
}
