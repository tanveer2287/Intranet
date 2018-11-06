using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrgWebAPI.Repository.Helpers;
using Core.Domain;
namespace OrgWebAPI.Repository.Mapping
{
    public abstract class EntityMap<TModel, TId> : EntityMappingConfiguration<TModel>
        where TModel : Entity<TId> where TId : struct
    {
        public override void Map(EntityTypeBuilder<TModel> builder)
        {
            builder.HasKey(t => t.Id);
            if (typeof(Guid).IsAssignableFrom(typeof(TId)))
                builder.Property(t => t.Id).ValueGeneratedOnAdd();
        }
    }
}
