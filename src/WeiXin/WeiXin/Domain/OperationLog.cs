using Infrastructure.Core.DDD;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeiXin.Domain
{

    /// <summary>
    /// 操作日志
    /// </summary>
    public partial class OperationLog: AggregateRoot
    {
        /// <summary>
        /// 操作IP
        /// </summary>
        [Required]
        [MaxLength(15)]
        public string Ip { get; set; }

        /// <summary>
        /// 接收人
        /// </summary>
        [MaxLength(15)]
        public string Telephone { get; set; }

        /// <summary>
        /// 对象类型
        /// </summary>
        [MaxLength(15)]
        public string ObjectType { get; set; }

        /// <summary>
        /// 对象ID
        /// </summary>
        [MaxLength(15)]
        public string ObjectID { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        [MaxLength(15)]
        public string Product { get; set; }

        /// <summary>
        /// 项目Id
        /// </summary>
        [MaxLength(15)]
        public string Project { get; set; }

        /// <summary>
        /// 动作
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// 操作者
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperationTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 操作内容
        /// </summary>
        public string OperationData { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
