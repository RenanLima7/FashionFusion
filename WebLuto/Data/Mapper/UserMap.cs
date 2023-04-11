using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebLuto.Models;

namespace WebLuto.Data.Mapper
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Username).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.Type).IsRequired();
        }
    }
}
