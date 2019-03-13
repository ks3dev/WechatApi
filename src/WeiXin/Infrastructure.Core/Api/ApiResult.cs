using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Core.Api
{
    public class ApiResult
    {
        [JsonProperty(PropertyName = "code")] public int Code { get; set; } = 200;

        [JsonProperty(PropertyName = "response")]
        public object Data { get; set; } = new { };
    }
    /// <summary>
    /// 异常响应信息
    /// </summary>
    public class ApiErrResult
    {
        public ApiErrResult()
        {
        }

        public ApiErrResult(int code, string msg)
        {
            this.Msg = msg;
            this.Code = code;
        }

        public ApiErrResult(int code, string msg, object errorData)
        {
            this.Msg = msg;
            this.Code = code;
            this.ErrorData = errorData;
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        [JsonProperty(PropertyName = "msg")]
        public string Msg { get; set; }

        /// <summary>
        /// 错误码
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public int Code { get; set; }

        /// <summary>
        /// 第三方平台返回的错误数据
        /// </summary>
        [JsonProperty(PropertyName = "error_data")]
        public object ErrorData { get; set; }
    }
}
