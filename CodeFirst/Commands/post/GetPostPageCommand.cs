using System.Collections.Generic;
using Boxed.AspNetCore;
using Boxed.Mapping;
using CodeFirst.Constants;
using CodeFirst.Repositories;
using CodeFirst.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Post = CodeFirst.Models.Post;

namespace CodeFirst.Commands
{
    public class GetPostPageCommand : IGetPostPageCommand
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper<Post, ViewModels.Post> _postMapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUrlHelper _urlHelper;

        public GetPostPageCommand(
            IPostRepository postRepository,
            IMapper<Post, ViewModels.Post> postMapper,
            IHttpContextAccessor httpContextAccessor,
            IUrlHelper urlHelper)
        {
            _postRepository = postRepository;
            _postMapper = postMapper;
            _httpContextAccessor = httpContextAccessor;
            _urlHelper = urlHelper;
        }

        public IActionResult Execute(PageOptions pageOptions)
        {
            var posts = _postRepository.GetPage(pageOptions.Page.Value, pageOptions.Count.Value);
            if (posts == null)
            {
                return new NotFoundResult();
            }

            var (totalCount, totalPages) = _postRepository.GetTotalPages(pageOptions.Count.Value);
            var postViewModels = _postMapper.MapList(posts);
            var page = new PageResult<ViewModels.Post>
            {
                Count = pageOptions.Count.Value,
                Items = postViewModels,
                Page = pageOptions.Page.Value,
                TotalCount = totalCount,
                TotalPages = totalPages
            };

            // Add the Link HTTP Header to add URL's to next, previous, first and last pages.
            // See https://tools.ietf.org/html/rfc5988#page-6
            // There is a standard list of link relation types e.g. next, previous, first and last.
            // See https://www.iana.org/assignments/link-relations/link-relations.xhtml
            _httpContextAccessor.HttpContext.Response.Headers.Add(
                "Link",
                GetLinkValue(page));

            return new OkObjectResult(page);
        }

        private string GetLinkValue(PageResult<ViewModels.Post> page)
        {
            var values = new List<string>(4);

            if (page.HasNextPage)
            {
                values.Add(GetLinkValueItem("next", page.Page + 1, page.Count));
            }

            if (page.HasPreviousPage)
            {
                values.Add(GetLinkValueItem("previous", page.Page - 1, page.Count));
            }

            values.Add(GetLinkValueItem("first", 1, page.Count));
            values.Add(GetLinkValueItem("last", page.TotalPages, page.Count));

            return string.Join(", ", values);
        }

        private string GetLinkValueItem(string rel, int page, int count)
        {
            var url = _urlHelper.AbsoluteRouteUrl(
                PostsControllerRoute.GetPostPage,
                new PageOptions { Page = page, Count = count });
            return $"<{url}>; rel=\"{rel}\"";
        }

    }

    public interface IGetPostPageCommand : ICommand<PageOptions>
    {
    }
}