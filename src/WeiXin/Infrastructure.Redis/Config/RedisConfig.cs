using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Redis.Config
{
    public class RedisConfig
    {
        /// <summary>
        /// Ip地址
        /// </summary>
        public string Ip { get; set; } = "127.0.0.1";

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; } = 6379;

        /// <summary>
        /// 储存的数据库索引
        /// </summary>
        public int DataBase { get; set; } = 0;

        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; } = "";

        /// <summary>
        /// Redis连接池连接数
        /// </summary>
        public int PoolSize { get; set; }
    }
}
