using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace WeiXin.Domain.Param
{
    /// <summary>
    /// 发送信息
    /// </summary>
    public class SendMsgParam
    {
        /// <summary>
        /// 好友名称(标识)
        /// </summary>
        [JsonProperty(PropertyName = "friendsname")]
        [Required]
        public string FriendsName { get; set; }

        /// <summary>
        /// 发送信息
        /// </summary>
        [JsonProperty(PropertyName = "msg")]
        [Required(ErrorMessage ="消息内容不得为空")]
        public string Msg { get; set; }
    }
}
