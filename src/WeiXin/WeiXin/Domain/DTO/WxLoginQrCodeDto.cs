using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeiXin.Domain.DTO
{

    public class WxLoginQrCodeDto
    {
        /// <summary>
        /// UUid
        /// </summary>
        [JsonProperty("uuid")]
        public string UUid { get; set; }

        /// <summary>
        /// QrCode Url
        /// </summary>
        [JsonProperty("url")]
        public string WxQrCodeUrl { get; set; }

        public static WxLoginQrCodeDto GetQrCodeDto(string uuid, string url)
        {
            return new WxLoginQrCodeDto
            {
                UUid = uuid,
                WxQrCodeUrl = url
            };
        }
    }
}
