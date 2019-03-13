using Infrastructure.Core.DDD;
using Infrastructure.Core.EF;
using System.Linq;
using WeiXin.Domain;
using WeiXin.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace WeiXin.Repositories.Infrastructure
{
    public class WxUsersRepository : Repository<WxUsers>, IWxUsersRepository
    {
        public WxUsersRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="uin"></param>
        /// <returns></returns>
        public WxUsers GetWxUsers(string uin)
        {
            return _dbContext.Set<WxUsers>().Include(c=>c.Detail).Include(c=>c.Friends).FirstOrDefault(x => x.Uin.Equals(uin));
        }
    }
}
