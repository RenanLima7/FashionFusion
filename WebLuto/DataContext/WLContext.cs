using Microsoft.EntityFrameworkCore;
using WebLuto.Data.Mapper;
using WebLuto.DataContext.Mapper;
using WebLuto.Models;
using WebLuto.Security;

namespace WebLuto.DataContext
{
    public class WLContext : DbContext
    {
        public WLContext(DbContextOptions<WLContext> options)
            : base(options)
        { }

        public WLContext() : base(GetDefaultOptions())
        { }

        public DbSet<User> User { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<ClientToken> ClientToken { get; set; }
        //public DbSet<Card> Card { get; set; }
        public DbSet<Sale> Sale { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new ClientMap());
            modelBuilder.ApplyConfiguration(new AddressMap());
            modelBuilder.ApplyConfiguration(new ClientTokenMap());
            modelBuilder.ApplyConfiguration(new SaleMap());
            //modelBuilder.ApplyConfiguration(new CardMap());

            base.OnModelCreating(modelBuilder);
        }

        private static DbContextOptions<WLContext> GetDefaultOptions()
        {
            var builder = new DbContextOptionsBuilder<WLContext>();
            builder.UseNpgsql(new Settings().DataBase);

            return builder.Options;
        }
    }
}
