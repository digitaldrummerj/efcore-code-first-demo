

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
    public class GetPostCommand : IGetPostCommand
    {
        private readonly IActionContextAccessor actionContextAccessor;
        private readonly IPostRepository postRepository;
        private readonly IMapper<Models.Post, Post> postMapper;

        public GetPostCommand(
            IActionContextAccessor actionContextAccessor,
            IPostRepository postRepository,
            IMapper<Models.Post, Post> postMapper)
        {
            this.actionContextAccessor = actionContextAccessor;
            this.postRepository = postRepository;
            this.postMapper = postMapper;
        }

        public IActionResult Execute(int postId)
        {
            var post = this.postRepository.Get(postId);
            if (post == null)
            {
                return new NotFoundResult();
            }

            var httpContext = this.actionContextAccessor.ActionContext.HttpContext;
            if (httpContext.Request.Headers.TryGetValue(HeaderNames.IfModifiedSince, out var stringValues))
            {
                if (DateTimeOffset.TryParse(stringValues, out var modifiedSince) &&
                    (modifiedSince >= post.Modified))
                {
                    return new StatusCodeResult(StatusCodes.Status304NotModified);
                }
            }

            var postViewModel = this.postMapper.Map(post);
            httpContext.Response.Headers.Add(HeaderNames.LastModified, post.Modified.ToString("R"));
            return new OkObjectResult(postViewModel);
        }
    }

    public interface IGetPostCommand : ICommand<int>
    {
    }
}