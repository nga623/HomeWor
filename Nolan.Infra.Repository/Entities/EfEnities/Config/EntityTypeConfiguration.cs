using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace Nolan.Infra.Repository.Entities.EfEnities.Config
{
    public abstract class EntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
       where TEntity : Entity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            var entityType = typeof(TEntity);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever().HasMaxLength(100);

            //if (typeof(IConcurrency).IsAssignableFrom(entityType))
            //{
            //    builder.Property("RowVersion").IsRequired().IsRowVersion().ValueGeneratedOnAddOrUpdate();
            //}

            if (typeof(ISoftDelete).IsAssignableFrom(entityType))
            {
                builder.Property("IsDeleted")
                       .HasDefaultValue(false);
                //builder.HasQueryFilter(d => EF.Property<bool>(d, "IsDeleted") == false);

                builder.Property("DeletedBy").HasMaxLength(50);
            }

            //if (typeof(IFullAuditInfo).IsAssignableFrom(entityType))
            //{
            //    builder.Property("ModifyBy").HasMaxLength(50);
            //}

            //if (typeof(IBasicAuditInfo).IsAssignableFrom(entityType))
            //{
            //    builder.Property("CreateBy").HasMaxLength(50);
            //}

            //if (typeof(EfFullAuditEntity).IsAssignableFrom(entityType))
            //{
            //    builder.Property("Description").HasMaxLength(500);
            //    builder.Property("DeletedBy").HasMaxLength(50);
            //}

            //if (typeof(ICusAudit).IsAssignableFrom(entityType))
            //{
            //    builder.Property("CustomerId").HasMaxLength(50);
            //}


        }
    }
}
