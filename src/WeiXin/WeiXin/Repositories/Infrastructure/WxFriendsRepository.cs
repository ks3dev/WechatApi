using Infrastructure.Core.DDD;
using Infrastructure.Core.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeiXin.Domain;
using WeiXin.Domain.Interfaces;

namespace WeiXin.Repositories.Infrastructure
{
    public class WxFriendsRepository : Repository<WxFriends>, IWxFriendsRepository
    {
        public WxFriendsRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public WxFriends GetByName(string uin, string friendusername)
        {
            return _dbContext.Set<WxFriends>().Where(m => m.UserName.Equals(friendusername) && m.UserUin.Equals(uin)).FirstOrDefault();
        }

        public WxFriends GetFriendByName(string name, string userUin)
        {
            var db = _dbContext.Set<WxFriends>();
            var friend= db.Where(m => m.RemarkName.Equals(name) && m.UserUin.Equals(userUin)).FirstOrDefault();
            if (friend == null) friend = db.Where(m => m.NickName.Equals(name) && m.UserUin.Equals(userUin)).FirstOrDefault();
            return friend;
        }
    }
}
