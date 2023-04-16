using Microsoft.EntityFrameworkCore;
using WebLuto.Data.Mapper;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new ClientMap());
            modelBuilder.ApplyConfiguration(new AddressMap());

            base.OnModelCreating(modelBuilder);
        }

        private static DbContextOptions<WLContext> GetDefaultOptions()
        {
            var builder = new DbContextOptionsBuilder<WLContext>();
            builder.UseSqlServer(new Settings().DataBase);

            return builder.Options;
        }
    }
}
