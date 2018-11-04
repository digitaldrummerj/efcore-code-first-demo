using System;
using Boxed.AspNetCore;
using Boxed.Mapping;
using CodeFirst.Repositories;
using CodeFirst.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Net.Http.Headers;

namespace CodeFirst.Commands
{
    public class GetBlogCommand : IGetBlogCommand
    {
        private readonly IActionContextAccessor actionContextAccessor;
        private readonly IBlogRepository blogRepository;
        private readonly IMapper<Models.Blog, Blog> blogMapper;

        public GetBlogCommand(
            IActionContextAccessor actionContextAccessor,
            IBlogRepository blogRepository,
            IMapper<Models.Blog, Blog> blogMapper)
        {
            this.actionContextAccessor = actionContextAccessor;
            this.blogRepository = blogRepository;
            this.blogMapper = blogMapper;
        }

        public IActionResult Execute(int blogId)
        {
            var blog = this.blogRepository.Get(blogId);
            if (blog == null)
            {
                return new NotFoundResult();
            }

            var httpContext = this.actionContextAccessor.ActionContext.HttpContext;
            if (httpContext.Request.Headers.TryGetValue(HeaderNames.IfModifiedSince, out var stringValues))
            {
                if (DateTimeOffset.TryParse(stringValues, out var modifiedSince) &&
                    (modifiedSince >= blog.Modified))
                {
                    return new StatusCodeResult(StatusCodes.Status304NotModified);
                }
            }

            var blogViewModel = this.blogMapper.Map(blog);
            httpContext.Response.Headers.Add(HeaderNames.LastModified, blog.Modified.ToString("R"));
            return new OkObjectResult(blogViewModel);
        }
    }

    public interface IGetBlogCommand : ICommand<int>
    {
    }
}