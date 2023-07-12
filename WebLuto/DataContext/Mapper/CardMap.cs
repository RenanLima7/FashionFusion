using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebLuto.Common;
using WebLuto.Models;

namespace WebLuto.Data.Mapper
{
    public class CardMap : BaseMapper<Card>
    {
        public CardMap()
        { }

        public override void Configure(EntityTypeBuilder<Card> builder)
        {
            base.Configure(builder);

            builder.Property(a => a.FullName).IsRequired();
            builder.Property(a => a.CVV).IsRequired();
            builder.Property(a => a.Number).IsRequired();
            builder.Property(a => a.ExpirationDate).IsRequired();

            builder.HasOne(a => a.Client)
                   .WithOne(c => c.Card)
                   .HasForeignKey<Client>(c => c.Id);
        }
    }
}
