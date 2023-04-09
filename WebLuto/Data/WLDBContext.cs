using Microsoft.EntityFrameworkCore;
using WebLuto.Data.Mapper;
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

        public DbSet<Product> Product { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new ProductMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
