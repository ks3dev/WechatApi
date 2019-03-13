using Infrastructure.Core.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeiXin.Domain;

namespace WeiXin.Repositories.Map
{
    public class WxUserDetailsMap : IEntityMap<WxUserDetails>
    {
        public void Map(EntityTypeBuilder<WxUserDetails> builder)
        {
            builder.ToTable("wxuserdetails");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedOnAdd();

            builder.Property(t => t.Hosts);
            builder.Property(t => t.WxKeysJson);
            builder.Property(t => t.UserUin);
            builder.Ignore(m => m.WxKeys);
        }
    }
}
