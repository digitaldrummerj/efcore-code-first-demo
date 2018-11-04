using CodeFirst.Models;

namespace CodeFirst.Repositories
{
    public class BlogRepository : RepositoryBase<Blog>, IBlogRepository
    {
        public BlogRepository(CodeFirstContext context) : base(context)
        {

        }
    }
}