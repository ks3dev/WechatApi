using Infrastructure.Core.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeiXin.Domain;

namespace WeiXin.Repositories.Map
{
    public class WxUsersMap : IEntityMap<WxUsers>
    {
        public void Map(EntityTypeBuilder<WxUsers> builder)
        {
            builder.ToTable("WxUsers");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedOnAdd();

            builder.Property(t => t.Uin);
            builder.Property(t => t.UserName);
            builder.Property(t => t.NickName);
            builder.Property(t => t.HeadImgUrl);
            builder.Property(t => t.PYInitial);
            builder.Property(t => t.PYQuanPin);
            builder.Property(t => t.StarFriend);
            builder.Property(t => t.Sex);
            builder.Property(t => t.CreateTime);
            builder.Property(t => t.UpdateTime);

            builder.HasOne(t => t.Detail).WithOne(m => m.Users).HasPrincipalKey<WxUsers>(m=>m.Uin).HasForeignKey<WxUserDetails>(m => m.UserUin);
            builder.HasMany(t => t.Friends).WithOne(m => m.User).HasPrincipalKey(m=>m.Uin).HasForeignKey(m => m.UserUin);

        }
    }
}
