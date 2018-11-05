using CodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeFirst
{
    public class CodeFirstContext : DbContext
    {
        public CodeFirstContext(DbContextOptions<CodeFirstContext> options) : base(options)
        {

        }

        public DbSet<Car> Car { get; set; }
        public DbSet<Blog> Blog { get; set; }
        public DbSet<Post> Post { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>()
                .HasIndex(b => b.BlogUrl)
                .IsUnique();

            modelBuilder.Entity<Post>()
                .HasIndex(b => b.Title);

            modelBuilder.Entity<Car>().HasIndex(b => new {b.Make, b.Model});

            modelBuilder.Entity<Blog>().HasQueryFilter(b => !b.IsDeleted);
        }
    }
}