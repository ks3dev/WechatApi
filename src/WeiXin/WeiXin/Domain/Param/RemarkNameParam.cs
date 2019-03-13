using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeiXin.Domain.Param
{
    /// <summary>
    /// 更改备注
    /// </summary>
    public class RemarkNameParam
    {
        /// <summary>
        /// 好友名称(标识)
        /// </summary>
        [JsonProperty(PropertyName = "friendsname")]
        [Required]
        public string FriendsName { get; set; }

        /// <summary>
        /// 备注名
        /// </summary>
        [JsonProperty(PropertyName = "RemarkName")]
        [Required(ErrorMessage ="备注名不得为空")]
        public string RemarkName { get; set; }
    }
}
