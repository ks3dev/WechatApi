using Infrastructure.Core.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeiXin.Domain;

namespace WeiXin.Repositories.Map
{
    public class OperationLogMap : IEntityMap<OperationLog>
    {
        public void Map(EntityTypeBuilder<OperationLog> builder)
        {
            builder.ToTable("operationlog");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedOnAdd();

            builder.Property(t => t.Id);
            builder.Property(t => t.Telephone);
            builder.Property(t => t.ObjectType);
            builder.Property(t => t.ObjectID);
            builder.Property(t => t.Product);
            builder.Property(t => t.Project);
            builder.Property(t => t.Action);
            builder.Property(t => t.UserName);
            builder.Property(t => t.OperationTime);
            builder.Property(t => t.Comment);
            builder.Property(t => t.OperationData);
            builder.Property(t => t.CreateTime);

        }
    }
}
