using CAR.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace CAR.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    // Määrab täpsuse decimal jaoks
        //    modelBuilder.Entity<Car>()
        //        .Property(c => c.Price)
        //        .HasPrecision(18, 2);  // 18 numbrit, 2 komakohta
        //}
    }
}
