using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeiXin.Domain.Param
{
    /// <summary>
    /// 操作记录
    /// </summary>
    public class WebHookParam
    {
        /// <summary>
        /// 对象类型，可以为空
        /// </summary>
        [JsonProperty(PropertyName = "objectType")]
        public string objectType { get; set; }

        /// <summary>
        /// 对象ID，可以为空
        /// </summary>
        [JsonProperty(PropertyName = "objectID")]
        public string objectID { get; set; }

        /// <summary>
        /// 关联产品ID，可以为空
        /// </summary>
        [JsonProperty(PropertyName = "product")]
        public string product { get; set; }

        /// <summary>
        /// 关联项目ID，可以为空
        /// </summary>
        [JsonProperty(PropertyName = "project")]
        public string project { get; set; }

        /// <summary>
        /// 动作，可以为空
        /// </summary>
        [JsonProperty(PropertyName = "action")]
        public string action { get; set; }

        /// <summary>
        /// 操作者，可以为空
        /// </summary>
        [JsonProperty(PropertyName = "actor")]
        public string actor { get; set; }

        /// <summary>
        /// 操作时间，可以为空
        /// </summary>
        [JsonProperty(PropertyName = "date")]
        public string date { get; set; }

        /// <summary>
        /// 备注，可以为空
        /// </summary>
        [JsonProperty(PropertyName = "comment")]
        public string comment { get; set; }

        /// <summary>
        /// 操作内容，包含操作对象的url，必选。
        /// </summary>
        [JsonProperty(PropertyName = "text")]
        [Required]
        public string text { get; set; }

        /// <summary>
        /// 接收人
        /// </summary>
        [JsonProperty(PropertyName = "telephone")]
        [Required(ErrorMessage ="接收人不得为空")]
        [RegularExpression(@"^1[3458][0-9]{9}$", ErrorMessage = "手机号格式不正确")]
        public string Telephone { get; set; }
    }
}
