using System;
using System.Linq;
using System.Reflection;
using CodeFirst.Models;
using CodeFirst.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CodeFirst
{
    public class CodeFirstContext : DbContext
    {
        private static readonly MethodInfo SetGlobalQueryForSoftDeleteMethodInfo = typeof(CodeFirstContext)
            .GetMethods(BindingFlags.Public | BindingFlags.Instance)
            .Single(t => t.IsGenericMethod && t.Name == "SetGlobalQueryForSoftDelete");

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

            SetGlobalQueryFilters(modelBuilder);

            modelBuilder.Entity<Post>()
                .HasOne(p => p.Blog)
                .WithMany(b => b.Posts)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Blog>()
                .Property(b => b.Created)
                .HasDefaultValueSql("getutcdate()");

            modelBuilder.Entity<Post>()
                .Property(b => b.Created)
                .HasDefaultValueSql("getutcdate()");

        }

        private void SetGlobalQueryFilters(ModelBuilder modelBuilder)
        {
            foreach (IMutableEntityType tp in modelBuilder.Model.GetEntityTypes())
            {
                Type t = tp.ClrType;

                // set global filters
                if (typeof(ISoftDelete).IsAssignableFrom(t))
                {
                    // softdeletable
                    MethodInfo method = SetGlobalQueryForSoftDeleteMethodInfo.MakeGenericMethod(t);
                    method.Invoke(this, new object[] { modelBuilder });
                }
            }
        }

        public void SetGlobalQueryForSoftDelete<T>(ModelBuilder builder) where T : class, ISoftDelete
        {
            builder.Entity<T>().HasQueryFilter(item => !item.IsDeleted);
        }

    }
}