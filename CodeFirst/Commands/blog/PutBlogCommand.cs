using Boxed.AspNetCore;
using Boxed.Mapping;
using CodeFirst.Repositories;
using CodeFirst.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Blog = CodeFirst.Models.Blog;

namespace CodeFirst.Commands
{
    public class PutBlogCommand : IPutBlogCommand
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper<Blog, ViewModels.Blog> _blogToBlogMapper;
        private readonly IMapper<SaveBlog, Blog> _saveBlogToBlogMapper;

        public PutBlogCommand(
            IBlogRepository blogRepository,
            IMapper<Blog, ViewModels.Blog> blogToBlogMapper,
            IMapper<SaveBlog, Blog> saveBlogToBlogMapper)
        {
            _blogRepository = blogRepository;
            _blogToBlogMapper = blogToBlogMapper;
            _saveBlogToBlogMapper = saveBlogToBlogMapper;
        }

        public IActionResult Execute(int blogId, SaveBlog saveBlog)
        {
            var blog = _blogRepository.Get(blogId);
            if (blog == null)
            {
                return new NotFoundResult();
            }

            _saveBlogToBlogMapper.Map(saveBlog, blog);
            _blogRepository.Update(blog);
            var blogViewModel = _blogToBlogMapper.Map(blog);

            return new OkObjectResult(blogViewModel);
        }

    }

    public interface IPutBlogCommand : ICommand<int, SaveBlog>
    {
    }
}