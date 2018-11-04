using CodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeFirst
{
    public class CodeFirstContext : DbContext
    {
        public CodeFirstContext(DbContextOptions<CodeFirstContext> options) : base(options)
        {

        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}