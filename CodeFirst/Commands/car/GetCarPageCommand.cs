using System.Collections.Generic;
using Boxed.AspNetCore;
using Boxed.Mapping;
using CodeFirst.Constants;
using CodeFirst.Repositories;
using CodeFirst.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Car = CodeFirst.Models.Car;

namespace CodeFirst.Commands
{
    public class GetCarPageCommand : IGetCarPageCommand
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper<Car, ViewModels.Car> _carMapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUrlHelper _urlHelper;

        public GetCarPageCommand(
            ICarRepository carRepository,
            IMapper<Car, ViewModels.Car> carMapper,
            IHttpContextAccessor httpContextAccessor,
            IUrlHelper urlHelper)
        {
            _carRepository = carRepository;
            _carMapper = carMapper;
            _httpContextAccessor = httpContextAccessor;
            _urlHelper = urlHelper;
        }

        public IActionResult Execute(PageOptions pageOptions)
        {
            var cars = _carRepository.GetPage(pageOptions.Page.Value, pageOptions.Count.Value);
            if (cars == null)
            {
                return new NotFoundResult();
            }

            var (totalCount, totalPages) = _carRepository.GetTotalPages(pageOptions.Count.Value);
            var carViewModels = _carMapper.MapList(cars);
            var page = new PageResult<ViewModels.Car>
            {
                Count = pageOptions.Count.Value,
                Items = carViewModels,
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

        private string GetLinkValue(PageResult<ViewModels.Car> page)
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
                CarsControllerRoute.GetCarPage,
                new PageOptions { Page = page, Count = count });
            return $"<{url}>; rel=\"{rel}\"";
        }
    }
}
