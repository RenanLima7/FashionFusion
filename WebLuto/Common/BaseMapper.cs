using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebLuto.Common
{
    public abstract class BaseMapper<E> : IEntityTypeConfiguration<E> where E : BaseEntity
    {
        public BaseMapper()
        { }

        public virtual void Configure(EntityTypeBuilder<E> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.CreationDate).IsRequired();
            builder.Property(x => x.UpdateDate);
            builder.Property(x => x.DeletionDate);
        }
    }
}
