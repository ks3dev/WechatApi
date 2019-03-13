using Infrastructure.Core.DDD;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeiXin.Domain
{
    /// <summary>
    /// 微信用户
    /// </summary>
    public partial class WxUsers: AggregateRoot
    {
        /// <summary>
        /// Uin
        /// </summary>
        public string Uin { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [MaxLength(1000)]
        public string UserName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [MaxLength(500)]
        public string NickName { get; set; }

        /// <summary>
        /// 头像地址
        /// </summary>
        [MaxLength(-1)]
        public string HeadImgUrl { get; set; }

        /// <summary>
        /// 拼音简写
        /// </summary>
        [MaxLength(-1)]
        public string PYInitial { get; set; }

        /// <summary>
        /// 拼音全拼
        /// </summary>
        [MaxLength(-1)]
        public string PYQuanPin { get; set; }

        /// <summary>
        /// 星标好友
        /// </summary>
        public bool StarFriend { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; set; }

        /// <summary>
        /// 微信详情
        /// </summary>
        public virtual WxUserDetails Detail { get; set; } = new WxUserDetails();

        /// <summary>
        /// 好友列表
        /// </summary>
        public virtual List<WxFriends> Friends { get; set; } = new List<WxFriends>();

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
