using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebLuto.Common;
using WebLuto.Models;

namespace WebLuto.Data.Mapper
{
    public class AddressMap : BaseMapper<Address>
    {
        public AddressMap()
        { }

        public override void Configure(EntityTypeBuilder<Address> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.ZipCode).IsRequired().HasMaxLength(10);
            builder.Property(x => x.AddressLine).IsRequired().HasMaxLength(250);
            builder.Property(x => x.AddressLineNumber).IsRequired().HasMaxLength(7);
            builder.Property(x => x.Neighborhood).IsRequired().HasMaxLength(100);

            builder.Property(x => x.ClientId).IsRequired();
        }
    }
}
