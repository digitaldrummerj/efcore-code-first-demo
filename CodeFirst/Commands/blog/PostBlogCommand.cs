using Boxed.AspNetCore;
using Boxed.Mapping;
using CodeFirst.Constants;
using CodeFirst.Repositories;
using CodeFirst.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Blog = CodeFirst.Models.Blog;

namespace CodeFirst.Commands
{
    public class PostBlogCommand : IPostBlogCommand
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper<Blog, ViewModels.Blog> _blogToBlogMapper;
        private readonly IMapper<SaveBlog, Blog> _saveBlogToBlogMapper;

        public PostBlogCommand(
            IBlogRepository blogRepository,
            IMapper<Blog, ViewModels.Blog> blogToBlogMapper,
            IMapper<SaveBlog, Blog> saveBlogToBlogMapper)
        {
            _blogRepository = blogRepository;
            _blogToBlogMapper = blogToBlogMapper;
            _saveBlogToBlogMapper = saveBlogToBlogMapper;
        }

        public IActionResult Execute(SaveBlog saveBlog)
        {
            var blog = _saveBlogToBlogMapper.Map(saveBlog);
            _blogRepository.Add(blog);
            var blogViewModel = _blogToBlogMapper.Map(blog);

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