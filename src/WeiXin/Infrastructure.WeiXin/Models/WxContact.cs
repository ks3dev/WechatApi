using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.WeiXin.Models
{
    public class WxContact
    {
        public int MemberCount { get; set; }

        public List<WxFriendItem> MemberList { get; set; }
    }
    public class WxFriendItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int Uin { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 头像地址
        /// </summary>
        public string HeadImgUrl { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string RemarkName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string Signature { get; set; }
        /// <summary>
        /// 星标好友
        /// </summary>
        public bool StarFriend { get; set; }
        /// <summary>
        /// 省份
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }

        #region 好友标识
        //好友：VerifyFlag=0  AttrStatus>0 
        //微信自己：ContactFlag=1 VerifyFlag>0  AttrStatus=0 
        //公众号：VerifyFlag>0  AttrStatus=0 ContactFlag=2051||ContactFlag=3
        //群聊：VerifyFlag=0  AttrStatus=0 
        public long VerifyFlag { get; set; }
        public long ContactFlag { get; set; }
        public long AttrStatus { get; set; }
        #endregion
    }
}
