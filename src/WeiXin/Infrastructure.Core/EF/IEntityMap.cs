using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Core.EF
{
    public interface IEntityMap<TEntityType> where TEntityType : class
    {
        void Map(EntityTypeBuilder<TEntityType> builder);
    }
}
