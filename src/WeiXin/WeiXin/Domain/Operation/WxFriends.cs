using Infrastructure.Core.DDD;
using WeiXin.Domain.Event;
using WeiXin.Domain.Interfaces;

namespace WeiXin.Domain
{
    public partial class WxFriends
    {
        #region 绑定手机号
        public void BindTelephone(IWxFriendsRepository repository,string telephone)
        {
            this.Telephone = telephone;
            repository.Update(this);
        }
        #endregion

        #region 更换备注名

        /// <summary>
        /// 更换备注名
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="remarkName"></param>
        public void SetRemarkName(IWxFriendsRepository repository, string remarkName)
        {
            this.RemarkName = remarkName;
            repository.Update(this);
        }
        #endregion
    }
}
