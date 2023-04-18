using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebLuto.Models;

namespace WebLuto.Data.Mapper
{
    public class ClientMap : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Email).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Password).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Salt).IsRequired();
            builder.Property(x => x.IsConfirmed);

            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(25);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(25);
            builder.Property(x => x.CPF).IsRequired().HasMaxLength(15);
            builder.Property(x => x.Phone).HasMaxLength(20);
            builder.Property(x => x.BirthDate);
            builder.Property(x => x.Avatar);
            builder.Property(x => x.CreationDate).IsRequired();
            builder.Property(x => x.UpdateDate);
            builder.Property(x => x.DeletionDate);
        }
    }
}
