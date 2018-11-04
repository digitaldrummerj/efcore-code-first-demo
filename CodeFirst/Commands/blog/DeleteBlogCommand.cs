using Boxed.AspNetCore;
using CodeFirst.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirst.Commands
{
    public class DeleteBlogCommand : IDeleteBlogCommand
    {
        private readonly IBlogRepository _blogRepository;

        public DeleteBlogCommand(IBlogRepository blogRepository) =>
            _blogRepository = blogRepository;

        public IActionResult Execute(int id)
        {
            var blog = _blogRepository.Get(id);
            if (blog == null)
            {
                return new NotFoundResult();
            }

            _blogRepository.Delete(blog);

            return new NoContentResult();
        }
    }

    public interface IDeleteBlogCommand : ICommand<int>
    {
    }
}