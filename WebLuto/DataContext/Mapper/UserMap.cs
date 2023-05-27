using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebLuto.Common;
using WebLuto.Models;

namespace WebLuto.Data.Mapper
{
    public class UserMap : BaseMapper<User>
    {
        public UserMap()
        { }

        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Username).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.Email).IsRequired();
        }
    }
}
