using Infrastructure.Core.DDD;
using Infrastructure.WeiXin.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WeiXin.Domain;

namespace WeiXin.Domain.DTO
{
    public class WxFriendsDto
    {
        [JsonProperty("friends")]
        public List<WxFriends> Friends { get; set; }

        [JsonProperty("publicaccount")]
        public List<WxFriends> PublicAccount { get; set; }

        [JsonProperty("groupchat")]
        public List<WxFriends> GroupChat { get; set; }

        #region 获取好友

        /// <summary>
        /// 获取好友
        /// </summary>
        /// <param name="query"></param>
        /// <param name="uin"></param>
        /// <returns></returns>
        public static WxFriendsDto GetFriends(IQuery<WxFriends> query,string uin)
        {
            var dbfriends = query.GetList(m => m.UserUin.Equals(uin));
            var friends = dbfriends.Where(m => m.VerifyFlag == 0 && m.AttrStatus > 0).ToList();
            var groupChat = dbfriends.Where(m => m.VerifyFlag == 0 && m.AttrStatus == 0).ToList();
            var publicAccount = dbfriends.Where(m => m.VerifyFlag > 0 && m.AttrStatus == 0 && (m.ContactFlag == 2015 || m.ContactFlag == 3)).ToList();
            return new WxFriendsDto
            {
                Friends = friends,
                GroupChat = groupChat,
                PublicAccount = publicAccount
            };
        }
        #endregion
    }
    public class WxFriendsItemDto
    {
        //TODO 转换格式
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
    }
}
