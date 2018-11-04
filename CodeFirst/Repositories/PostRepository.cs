using CodeFirst.Models;

namespace CodeFirst.Repositories
{
    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(CodeFirstContext context) : base(context)
        {

        }
    }
}