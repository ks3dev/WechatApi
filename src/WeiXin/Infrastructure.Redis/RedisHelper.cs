using Infrastructure.Redis.Config;
using Infrastructure.Redis.Interface;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Collections.Generic;

namespace Infrastructure.Redis
{
    public class RedisHelper:IRedisHelper
    {
        public RedisConfig _redisConfig { get; set; }
        /// <summary>
        /// 连接对象
        /// </summary>
        private IDatabase _dataBase { get; set; }
        public RedisHelper(IOptionsSnapshot<RedisConfig> redisConfig)
        {
            //127.0.0.1:16379,defaultDatabase=2,password=asdd8s7w5ada9t
            string context = string.Format("{0}:{1},defaultDatabase={2},password={3}",
                redisConfig.Value.Ip,
                redisConfig.Value.Port,
                redisConfig.Value.DataBase,
                redisConfig.Value.Password);
            var Connect = ConnectionMultiplexer.Connect(context);
            _dataBase = Connect.GetDatabase();
        }

        #region 添加String
        //加锁
        public readonly static object _locker = new object();

        /// <summary>
        /// 添加String
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetString(string key,string value)
        {
            lock (_locker)
            {
                _dataBase.StringSet(key, value);
            }
        }
        #endregion

        #region 获取String

        /// <summary>
        /// 获取String
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetString(string key)
        {
            return _dataBase.StringGet(key);
        }

        #endregion

        #region 添加Hash
        public void SetHash(string tablename, string key, string value)
        {
            List<HashEntry> listHashEntry = new List<HashEntry> {
                new HashEntry(key,value)
            };
            _dataBase.HashSet(tablename, listHashEntry.ToArray());
        }
        public void SetHash(string tablename,object value)
        {
            List<HashEntry> listHashEntry = new List<HashEntry>();

            var valueType = value.GetType();
            var valueprops = valueType.GetProperties();
            foreach (var itemprop in valueprops)
            {
                var hashname = itemprop.Name;
                var hashvalue = JsonConvert.SerializeObject(itemprop.GetValue(value));
                listHashEntry.Add(new HashEntry(hashname, hashvalue));
            }

            _dataBase.HashSet(tablename, listHashEntry.ToArray());
        }
        #endregion

        #region 获取Hash
        public string GetHash(string tablename,string key)
        {
            return _dataBase.HashGet(tablename,key);
        }
        #endregion
    }
}
