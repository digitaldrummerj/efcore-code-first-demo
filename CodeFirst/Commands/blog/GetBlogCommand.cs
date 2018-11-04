using System;
using Boxed.AspNetCore;
using Boxed.Mapping;
using CodeFirst.Models;
using CodeFirst.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Net.Http.Headers;

namespace CodeFirst.Commands
{
    public class GetBlogCommand : IGetBlogCommand
    {
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper<Blog, ViewModels.Blog> _blogMapper;

        public GetBlogCommand(
            IActionContextAccessor actionContextAccessor,
            IBlogRepository blogRepository,
            IMapper<Blog, ViewModels.Blog> blogMapper)
        {
            _actionContextAccessor = actionContextAccessor;
            _blogRepository = blogRepository;
            _blogMapper = blogMapper;
        }

        public IActionResult Execute(int blogId)
        {
            var blog = _blogRepository.Get(blogId);
            if (blog == null)
            {
                return new NotFoundResult();
            }

            var httpContext = _actionContextAccessor.ActionContext.HttpContext;
            if (httpContext.Request.Headers.TryGetValue(HeaderNames.IfModifiedSince, out var stringValues))
            {
                if (DateTimeOffset.TryParse(stringValues, out var modifiedSince) &&
                    (modifiedSince >= blog.Modified))
                {
                    return new StatusCodeResult(StatusCodes.Status304NotModified);
                }
            }

            var blogViewModel = _blogMapper.Map(blog);
            httpContext.Response.Headers.Add(HeaderNames.LastModified, blog.Modified.ToString("R"));
            return new OkObjectResult(blogViewModel);
        }
    }

    public interface IGetBlogCommand : ICommand<int>
    {
    }
}