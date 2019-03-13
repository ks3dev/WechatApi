using Infrastructure.Core.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeiXin.Domain;

namespace WeiXin.Repositories.Map
{
    public class WxFriendsMap : IEntityMap<WxFriends>
    {
        public void Map(EntityTypeBuilder<WxFriends> builder)
        {
            builder.ToTable("wxfriends");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedOnAdd();

            builder.Property(t => t.Uin);
            builder.Property(t => t.UserUin);
            builder.Property(t => t.UserName);
            builder.Property(t => t.NickName);
            builder.Property(t => t.HeadImgUrl);
            builder.Property(t => t.Province);
            builder.Property(t => t.City);
            builder.Property(t => t.Telephone);
            builder.Property(t => t.ContactFlag);
            builder.Property(t => t.AttrStatus);
            builder.Property(t => t.VerifyFlag);
            builder.Property(t => t.RemarkName);
            builder.Property(t => t.Sex);
            builder.Property(t => t.Signature);
            builder.Property(t => t.StarFriend);
            builder.Property(t => t.CreateTime);
        }
    }
}
