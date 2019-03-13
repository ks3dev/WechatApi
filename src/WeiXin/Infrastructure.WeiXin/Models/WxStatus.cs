using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.WeiXin.Models
{
    /// <summary>
    /// 微信状态
    /// </summary>
    public class WxStatus
    {
        /// <summary>
        /// 登录状态 0正常,1100 失败/登出微信,1101 地址错误,1102 Cookie错误
        /// </summary>
        public string RetCode { get; set; }

        /// <summary>
        /// 信息状态 0 正常 2 新的消息 7 进入/离开聊天界面
        /// </summary>
        public string Selector { get; set; }
    }
}
