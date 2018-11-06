#region

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace OrgWebAPI.Repository.Helpers
{
    public interface IEntityMappingConfiguration
    {
        void Map(ModelBuilder b);
    }

    public interface IEntityMappingConfiguration<T> : IEntityMappingConfiguration where T : class
    {
        void Map(EntityTypeBuilder<T> builder);
    }

    public abstract class EntityMappingConfiguration<TEntity> : IEntityMappingConfiguration<TEntity>
        where TEntity : class
    {
        public abstract void Map(EntityTypeBuilder<TEntity> builder);

        public void Map(ModelBuilder b)
        {
            Map(b.Entity<TEntity>());
        }
    }
}