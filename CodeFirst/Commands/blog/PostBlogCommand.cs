using Boxed.AspNetCore;
using Boxed.Mapping;
using CodeFirst.Constants;
using CodeFirst.Repositories;
using CodeFirst.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirst.Commands
{
    public class PostBlogCommand : IPostBlogCommand
    {
        private readonly IBlogRepository blogRepository;
        private readonly IMapper<Models.Blog, Blog> blogToBlogMapper;
        private readonly IMapper<SaveBlog, Models.Blog> saveBlogToBlogMapper;

        public PostBlogCommand(
            IBlogRepository blogRepository,
            IMapper<Models.Blog, Blog> blogToBlogMapper,
            IMapper<SaveBlog, Models.Blog> saveBlogToBlogMapper)
        {
            this.blogRepository = blogRepository;
            this.blogToBlogMapper = blogToBlogMapper;
            this.saveBlogToBlogMapper = saveBlogToBlogMapper;
        }

        public IActionResult Execute(SaveBlog saveBlog)
        {
            var blog = this.saveBlogToBlogMapper.Map(saveBlog);
            this.blogRepository.Add(blog);
            var blogViewModel = this.blogToBlogMapper.Map(blog);

            return new CreatedAtRouteResult(
                BlogsControllerRoute.GetBlog,
                new { id = blogViewModel.Id },
                blogViewModel);
        }
    }

    public interface IPostBlogCommand : ICommand<SaveBlog>
    {
    }
}