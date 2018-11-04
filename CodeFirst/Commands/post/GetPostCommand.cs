

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
    public class GetPostCommand : IGetPostCommand
    {
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly IPostRepository _postRepository;
        private readonly IMapper<Post, ViewModels.Post> _postMapper;

        public GetPostCommand(
            IActionContextAccessor actionContextAccessor,
            IPostRepository postRepository,
            IMapper<Post, ViewModels.Post> postMapper)
        {
            _actionContextAccessor = actionContextAccessor;
            _postRepository = postRepository;
            _postMapper = postMapper;
        }

        public IActionResult Execute(int postId)
        {
            var post = _postRepository.Get(postId);
            if (post == null)
            {
                return new NotFoundResult();
            }

            var httpContext = _actionContextAccessor.ActionContext.HttpContext;
            if (httpContext.Request.Headers.TryGetValue(HeaderNames.IfModifiedSince, out var stringValues))
            {
                if (DateTimeOffset.TryParse(stringValues, out var modifiedSince) &&
                    (modifiedSince >= post.Modified))
                {
                    return new StatusCodeResult(StatusCodes.Status304NotModified);
                }
            }

            var postViewModel = _postMapper.Map(post);
            httpContext.Response.Headers.Add(HeaderNames.LastModified, post.Modified.ToString("R"));
            return new OkObjectResult(postViewModel);
        }
    }

    public interface IGetPostCommand : ICommand<int>
    {
    }
}