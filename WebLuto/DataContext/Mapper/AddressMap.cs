using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebLuto.Models;

namespace WebLuto.Data.Mapper
{
    public class AddressMap : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ClientId).IsRequired();
            builder.Property(x => x.ZipCode).IsRequired().HasMaxLength(10);
            builder.Property(x => x.AddressLine).IsRequired().HasMaxLength(250);
            builder.Property(x => x.AddressLineNumber).IsRequired().HasMaxLength(7);
            builder.Property(x => x.Neighborhood).IsRequired().HasMaxLength(100);
            builder.Property(x => x.CreationDate).IsRequired();
            builder.Property(x => x.UpdateDate);
            builder.Property(x => x.DeletionDate);
        }
    }
}
