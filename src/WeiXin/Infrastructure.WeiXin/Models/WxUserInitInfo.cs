using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.WeiXin.Models
{
    public class ContactListItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int Uin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string HeadImgUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ContactFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int MemberCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<WxUserInfo> MemberList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RemarkName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int HideInputBarFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Sex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Signature { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int VerifyFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int OwnerUin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PYInitial { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PYQuanPin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RemarkPYInitial { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RemarkPYQuanPin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int StarFriend { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int AppAccountFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Statues { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int AttrStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Alias { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int SnsFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int UniFriend { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ChatRoomId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string KeyWord { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string EncryChatRoomId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int IsOwner { get; set; }
    }

    public class ListItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int Key { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Val { get; set; }
    }

    public class SyncKey
    {
        /// <summary>
        /// 
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ListItem> List { get; set; }
    }

    public class WxUserInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public long Uin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string HeadImgUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PYInitial { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PYQuanPin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int HideInputBarFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool StarFriend { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Sex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Signature { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int AppAccountFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int VerifyFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ContactFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int WebWxPluginSwitch { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int HeadImgFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int SnsFlag { get; set; }
    }

    public class MPArticleListItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Digest { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Cover { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Url { get; set; }
    }

    public class MPSubscribeMsgListItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int MPArticleCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<MPArticleListItem> MPArticleList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string NickName { get; set; }
    }

    /// <summary>
    /// 微信初始化信息
    /// </summary>
    public class WxUserInitInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ContactListItem> ContactList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public SyncKey SyncKey { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public WxUserInfo User { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ChatSet { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SKey { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ClientVersion { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int SystemTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int GrayScale { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int InviteStartCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int MPSubscribeMsgCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<MPSubscribeMsgListItem> MPSubscribeMsgList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ClickReportInterval { get; set; }
    }
}
