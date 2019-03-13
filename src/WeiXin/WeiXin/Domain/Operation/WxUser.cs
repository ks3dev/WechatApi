using Infrastructure.Core.DDD;
using Infrastructure.WeiXin.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WeiXin.Domain.Interfaces;

namespace WeiXin.Domain
{
    public partial class WxUsers
    {
        #region 设置微信Keys

        /// <summary>
        /// 设置微信Keys
        /// </summary>
        /// <param name="user"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public void SetWxXmlKeys()
        {
        }
        #endregion

        #region 设置微信操作域名

        /// <summary>
        /// 设置微信操作域名
        /// </summary>
        /// <param name="hosts"></param>
        public void SetWxHosts(string hosts)
        {
            this.Detail.Hosts = hosts;
        }
        #endregion

        #region 设置基础信息
        public static WxUsers SetWxUserInfo(IWxUsersRepository repository, WxUserInfo userModel, PassTicketXmlInfo keys)
        {
            var result=repository.GetWxUsers(keys.wxuin)?? new WxUsers();
            result.Uin = keys.wxuin;
            result.UserName = userModel.UserName;
            result.NickName = userModel.NickName;
            result.HeadImgUrl = userModel.HeadImgUrl;
            result.PYInitial = userModel.PYInitial;
            result.PYQuanPin = userModel.PYQuanPin;
            result.StarFriend = userModel.StarFriend;
            result.Sex = userModel.Sex;
            //设置keys
            result.Detail.WxKeys.Ret = keys.ret;
            result.Detail.WxKeys.Message = keys.message;
            result.Detail.WxKeys.Skey = keys.skey;
            result.Detail.WxKeys.WxSid = keys.wxsid;
            result.Detail.WxKeys.WxUin = keys.wxuin;
            result.Detail.WxKeys.PassTicket = keys.pass_ticket;
            result.Detail.WxKeys.Isgrayscale = keys.isgrayscale;
            result.Detail.WxKeysJson = JsonConvert.SerializeObject(result.Detail.WxKeys);

            if (result.Id<=0)
            {
                repository.Add(result);
            }
            else
            {
                repository.Update(result);
            }
            return result;
        }
        #endregion
        
        #region 同步好友

        /// <summary>
        /// 同步好友
        /// </summary>
        /// <returns></returns>
        public void SyncFriends(IWxFriendsRepository repository, WxContact wxContact)
        {
            for (int i=0;i< this.Friends.Count;i++)
            {
                var itemfirend = this.Friends[i];
                #region 查询
                Expression<Func<WxFriendItem, bool>> where = m => true;
                if (!String.IsNullOrEmpty(itemfirend.RemarkName))
                {
                    where = m => m.RemarkName.Equals(itemfirend.RemarkName);
                }
                else if (!String.IsNullOrEmpty(itemfirend.NickName))
                {
                    where = m => m.NickName.Equals(itemfirend.NickName);
                }
                else
                {
                    //TODO 备注和昵称均为空
                    continue;
                }
                var Friend = wxContact.MemberList.FirstOrDefault(where.Compile());
                #endregion
                if (Friend == null)
                {
                    //微信端删除好友
                    this.Friends.Remove(itemfirend);
                }
                else
                {
                    //同步好友信息
                    itemfirend.UserName = Friend.UserName;
                    itemfirend.StarFriend = Friend.StarFriend;
                    itemfirend.Province = Friend.Province;
                    itemfirend.City = Friend.City;
                    itemfirend.Sex = Friend.Sex;
                    itemfirend.HeadImgUrl = Friend.HeadImgUrl;
                    itemfirend.Signature = Friend.Signature;
                    itemfirend.NickName = Friend.NickName;
                    itemfirend.RemarkName = Friend.RemarkName;
                    wxContact.MemberList.Remove(Friend);
                }
            }
            if (wxContact.MemberList != null)
            {
                //添加好友
                this.Friends.AddRange(wxContact.MemberList.Select(m => new WxFriends
                {
                    UserUin = this.Uin,
                    Uin = m.Uin,
                    UserName = m.UserName,
                    StarFriend = m.StarFriend,
                    Province = m.Province,
                    City = m.City,
                    Sex = m.Sex,
                    HeadImgUrl = m.HeadImgUrl,
                    Signature = m.Signature,
                    ContactFlag = m.ContactFlag,
                    VerifyFlag = m.VerifyFlag,
                    AttrStatus = m.AttrStatus,
                    NickName = m.NickName,
                    RemarkName = m.RemarkName,
                }));
            }
        }
        #endregion
    }
}
