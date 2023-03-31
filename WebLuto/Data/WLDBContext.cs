using Microsoft.EntityFrameworkCore;
using WebLuto.Mapper;
using WebLuto.Models;

namespace WebLuto.Data
{
    public class WLDBContext : DbContext
    {
        public WLDBContext(DbContextOptions<WLDBContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        //public DbSet<Product> Product { get; set; }
        //public DbSet<Client> Client { get; set; }
        //public DbSet<Payment> Payment { get; set; }
        //public DbSet<Sale> Sale { get; set; }
        //public DbSet<Address> Address { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            //modelBuilder.ApplyConfiguration(new ProductMap());
            //modelBuilder.ApplyConfiguration(new ClientMap());
            //modelBuilder.ApplyConfiguration(new PaymentMap());
            //modelBuilder.ApplyConfiguration(new SaleMap());
            //modelBuilder.ApplyConfiguration(new AddressMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
