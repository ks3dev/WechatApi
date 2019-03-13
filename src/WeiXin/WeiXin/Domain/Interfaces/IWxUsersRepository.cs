using Infrastructure.Core.DDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeiXin.Domain.Interfaces
{
    //微信用户
    public interface IWxUsersRepository : IRepository<WxUsers>
    {
        WxUsers GetWxUsers(string uin);
    }
}
