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

        public WxFriends GetFriendByTelephone(string telephone)
        {
            return _dbContext.Set<WxFriends>().Where(m => m.Telephone.Equals(telephone)).FirstOrDefault();
        }
    }
}
