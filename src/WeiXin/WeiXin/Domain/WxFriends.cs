using Infrastructure.Core.DDD;
using System;
using System.ComponentModel.DataAnnotations;

namespace WeiXin.Domain
{
    public partial class WxFriends: AggregateRoot
    {
        /// <summary>
        /// 
        /// </summary>
        public int Uin { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [MaxLength(11)]
        public string Telephone { get; set; }

        /// <summary>
        /// 账号
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
        /// 联系标识
        /// </summary>
        public long ContactFlag { get; set; }
        public long VerifyFlag { get; set; }
        public long AttrStatus { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string RemarkName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        [MaxLength(255)]
        public string Signature { get; set; }

        /// <summary>
        /// 星标好友
        /// </summary>
        public bool StarFriend { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        [MaxLength(8)]
        public string Province { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        [MaxLength(30)]
        public string City { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 用户标识
        /// </summary>
        public string UserUin { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary>
        public WxUsers User { get; set; }
    }
}
