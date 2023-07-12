using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebLuto.Common;
using WebLuto.Models;

namespace WebLuto.Data.Mapper
{
    public class SaleMap : BaseMapper<Sale>
    {
        public override void Configure(EntityTypeBuilder<Sale> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.TotalValue).IsRequired();
        }
    }
}