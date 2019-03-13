using Infrastructure.Core.DDD;
using System.Collections.Generic;

namespace WeiXin.Domain.Interfaces
{
    public interface IWxFriendsRepository : IRepository<WxFriends>
    {
        WxFriends GetByName(string uin, string friendusername);
        WxFriends GetFriendByTelephone(string telephone);
    }
}
