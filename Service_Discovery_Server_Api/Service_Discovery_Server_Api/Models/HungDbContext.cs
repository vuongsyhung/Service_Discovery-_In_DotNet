using Microsoft.EntityFrameworkCore;


namespace Service_Discovery_Server_Api.Models
{
    public class HungDbContext : DbContext
    {
        public HungDbContext(DbContextOptions<HungDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Customer> Customers { get; set; }        
    }
}
