using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace WeiXin.Domain.Param
{
    /// <summary>
    /// 绑定手机号
    /// </summary>
    public class BindTelephoneParam
    {
        /// <summary>
        /// 好友名称(标识)
        /// </summary>
        [JsonProperty(PropertyName = "friendsname")]
        [Required]
        public string FriendsName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [JsonProperty(PropertyName = "telephone")]
        [Required(ErrorMessage ="手机号不得为空")]
        [RegularExpression(@"^1[3458][0-9]{9}$", ErrorMessage = "手机号格式不正确")]
        public string Telephone { get; set; }
    }
}
