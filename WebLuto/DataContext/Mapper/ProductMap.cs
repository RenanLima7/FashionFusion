using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebLuto.Models;

namespace WebLuto.Data.Mapper
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Type).IsRequired();
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.Dimension).HasMaxLength(15);
            builder.Property(x => x.Image);
            builder.Property(x => x.CreationDate).IsRequired();
            builder.Property(x => x.UpdateDate);
            builder.Property(x => x.DeletionDate);
        }
    }
}
