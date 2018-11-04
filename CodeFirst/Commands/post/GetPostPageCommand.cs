using System.Collections.Generic;
using Boxed.AspNetCore;
using Boxed.Mapping;
using CodeFirst.Constants;
using CodeFirst.Repositories;
using CodeFirst.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirst.Commands
{
    public class GetPostPageCommand : IGetPostPageCommand
    {
        private readonly IPostRepository postRepository;
        private readonly IMapper<Models.Post, Post> postMapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUrlHelper urlHelper;

        public GetPostPageCommand(
            IPostRepository postRepository,
            IMapper<Models.Post, Post> postMapper,
            IHttpContextAccessor httpContextAccessor,
            IUrlHelper urlHelper)
        {
            this.postRepository = postRepository;
            this.postMapper = postMapper;
            this.httpContextAccessor = httpContextAccessor;
            this.urlHelper = urlHelper;
        }

        public IActionResult Execute(PageOptions pageOptions)
        {
            var posts = this.postRepository.GetPage(pageOptions.Page.Value, pageOptions.Count.Value);
            if (posts == null)
            {
                return new NotFoundResult();
            }

            var (totalCount, totalPages) = this.postRepository.GetTotalPages(pageOptions.Count.Value);
            var postViewModels = this.postMapper.MapList(posts);
            var page = new PageResult<Post>()
            {
                Count = pageOptions.Count.Value,
                Items = postViewModels,
                Page = pageOptions.Page.Value,
                TotalCount = totalCount,
                TotalPages = totalPages,
            };

            // Add the Link HTTP Header to add URL's to next, previous, first and last pages.
            // See https://tools.ietf.org/html/rfc5988#page-6
            // There is a standard list of link relation types e.g. next, previous, first and last.
            // See https://www.iana.org/assignments/link-relations/link-relations.xhtml
            this.httpContextAccessor.HttpContext.Response.Headers.Add(
                "Link",
                this.GetLinkValue(page));

            return new OkObjectResult(page);
        }

        private string GetLinkValue(PageResult<Post> page)
        {
            var values = new List<string>(4);

            if (page.HasNextPage)
            {
                values.Add(this.GetLinkValueItem("next", page.Page + 1, page.Count));
            }

            if (page.HasPreviousPage)
            {
                values.Add(this.GetLinkValueItem("previous", page.Page - 1, page.Count));
            }

            values.Add(this.GetLinkValueItem("first", 1, page.Count));
            values.Add(this.GetLinkValueItem("last", page.TotalPages, page.Count));

            return string.Join(", ", values);
        }

        private string GetLinkValueItem(string rel, int page, int count)
        {
            var url = this.urlHelper.AbsoluteRouteUrl(
                PostsControllerRoute.GetPostPage,
                new PageOptions { Page = page, Count = count });
            return $"<{url}>; rel=\"{rel}\"";
        }

    }

    public interface IGetPostPageCommand : ICommand<PageOptions>
    {
    }
}