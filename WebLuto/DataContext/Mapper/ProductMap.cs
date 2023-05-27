using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebLuto.Common;
using WebLuto.Models;

namespace WebLuto.Data.Mapper
{
    public class ProductMap : BaseMapper<Product>
    {
        public ProductMap()
        { }

        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Type).IsRequired();
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.Dimension).HasMaxLength(15);
            builder.Property(x => x.Image);
        }
    }
}
