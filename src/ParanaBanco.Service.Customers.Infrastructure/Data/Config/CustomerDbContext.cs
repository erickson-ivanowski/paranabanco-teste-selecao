using Microsoft.EntityFrameworkCore;

namespace ParanaBanco.Service.Customers.Infrastructure.Data.Config
{
    public class CustomersDbContext : DbContext
    {
        public CustomersDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Models.Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Customer>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
