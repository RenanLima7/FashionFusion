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

            builder.Property(a => a.ZipCode).IsRequired().HasMaxLength(10);
            builder.Property(a => a.AddressLine).IsRequired().HasMaxLength(250);
            builder.Property(a => a.AddressLineNumber).IsRequired().HasMaxLength(7);
            builder.Property(a => a.Neighborhood).IsRequired().HasMaxLength(100);

            builder.Property(a => a.ClientId).IsRequired();
            builder.HasOne(a => a.Client)
                   .WithOne(c => c.Address)
                   .HasForeignKey<Client>(c => c.Id);
        }
    }
}
