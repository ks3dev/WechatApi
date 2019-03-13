using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Redis.Interface
{
    public interface IRedisHelper
    {
        void SetString(string key,string value);
        string GetString(string key);
        void SetHash(string tablename, string key, string value);
        void SetHash(string tablename, object value);
        string GetHash(string tablename, string key);
    }
}
