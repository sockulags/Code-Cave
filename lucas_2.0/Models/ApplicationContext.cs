using Microsoft.EntityFrameworkCore;
using System.Data;

namespace lucas_2._0.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options)
        {
            
        }

        public ApplicationContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Specificerar vilken datatyp databasen ska använda för en specifik kkolumn
            modelBuilder.Entity<Category>()
                .HasMany(p => p.SubCategories)
                .WithOne(p => p.Category);

            modelBuilder.Entity<SubCategory>()
                .HasMany(p => p.Posts)
                .WithOne(p => p.SubCategory);

           base.OnModelCreating(modelBuilder);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}
