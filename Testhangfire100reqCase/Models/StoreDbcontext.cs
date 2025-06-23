using Microsoft.EntityFrameworkCore;

namespace Testhangfire100reqCase.Models
{
    public class StoreDbcontext :DbContext
    {

        public StoreDbcontext(DbContextOptions<StoreDbcontext> options) : base(options)
        {
        }
        public DbSet<Orders> Orders { get; set; } 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Orders>().HasKey(o => o.Id);
            modelBuilder.Entity<Orders>().Property(o => o.CustomerName).IsRequired();
            modelBuilder.Entity<Orders>().Property(o => o.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}
