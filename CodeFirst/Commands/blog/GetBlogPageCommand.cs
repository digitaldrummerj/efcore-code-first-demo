using System.Collections.Generic;
using Boxed.AspNetCore;
using Boxed.Mapping;
using CodeFirst.Constants;
using CodeFirst.Repositories;
using CodeFirst.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Blog = CodeFirst.Models.Blog;

namespace CodeFirst.Commands
{
    public class GetBlogPageCommand : IGetBlogPageCommand
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper<Blog, ViewModels.Blog> _blogMapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUrlHelper _urlHelper;

        public GetBlogPageCommand(
            IBlogRepository blogRepository,
            IMapper<Blog, ViewModels.Blog> blogMapper,
            IHttpContextAccessor httpContextAccessor,
            IUrlHelper urlHelper)
        {
            _blogRepository = blogRepository;
            _blogMapper = blogMapper;
            _httpContextAccessor = httpContextAccessor;
            _urlHelper = urlHelper;
        }

        public IActionResult Execute(PageOptions pageOptions)
        {
            var blogs = _blogRepository.GetPage(pageOptions.Page.Value, pageOptions.Count.Value);
            if (blogs == null)
            {
                return new NotFoundResult();
            }

            var (totalCount, totalPages) = _blogRepository.GetTotalPages(pageOptions.Count.Value);
            var blogViewModels = _blogMapper.MapList(blogs);
            var page = new PageResult<ViewModels.Blog>
            {
                Count = pageOptions.Count.Value,
                Items = blogViewModels,
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

        private string GetLinkValue(PageResult<ViewModels.Blog> page)
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
                BlogsControllerRoute.GetBlogPage,
                new PageOptions { Page = page, Count = count });
            return $"<{url}>; rel=\"{rel}\"";
        }

    }

    public interface IGetBlogPageCommand : ICommand<PageOptions>
    {
    }
}