using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebLuto.Models;

namespace WebLuto.DataContext.Mapper
{
    public class ClientTokenMap : IEntityTypeConfiguration<ClientToken>
    {
        public void Configure(EntityTypeBuilder<ClientToken> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ClientId).IsRequired();
            builder.Property(x => x.Token).IsRequired().HasMaxLength(10);
            builder.Property(x => x.CreationDate).IsRequired();
            builder.Property(x => x.UpdateDate);
            builder.Property(x => x.DeletionDate);
        }
    }
}
