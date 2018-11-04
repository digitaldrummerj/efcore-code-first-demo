using Boxed.AspNetCore;
using Boxed.Mapping;
using CodeFirst.Repositories;
using CodeFirst.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirst.Commands
{
    public class PutBlogCommand : IPutBlogCommand
    {
        private readonly IBlogRepository blogRepository;
        private readonly IMapper<Models.Blog, Blog> blogToBlogMapper;
        private readonly IMapper<SaveBlog, Models.Blog> saveBlogToBlogMapper;

        public PutBlogCommand(
            IBlogRepository blogRepository,
            IMapper<Models.Blog, Blog> blogToBlogMapper,
            IMapper<SaveBlog, Models.Blog> saveBlogToBlogMapper)
        {
            this.blogRepository = blogRepository;
            this.blogToBlogMapper = blogToBlogMapper;
            this.saveBlogToBlogMapper = saveBlogToBlogMapper;
        }

        public IActionResult Execute(int blogId, SaveBlog saveBlog)
        {
            var blog = this.blogRepository.Get(blogId);
            if (blog == null)
            {
                return new NotFoundResult();
            }

            this.saveBlogToBlogMapper.Map(saveBlog, blog);
            this.blogRepository.Update(blog);
            var blogViewModel = this.blogToBlogMapper.Map(blog);

            return new OkObjectResult(blogViewModel);
        }

    }

    public interface IPutBlogCommand : ICommand<int, SaveBlog>
    {
    }
}