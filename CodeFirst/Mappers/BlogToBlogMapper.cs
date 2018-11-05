using System;
using Boxed.AspNetCore;
using Boxed.Mapping;
using CodeFirst.Constants;
using CodeFirst.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirst.Mappers
{
    public class BlogToBlogMapper : IMapper<Blog, ViewModels.Blog>
    {
        private readonly IUrlHelper _urlHelper;

        public BlogToBlogMapper(IUrlHelper urlHelper) => _urlHelper = urlHelper;

        public void Map(Blog source, ViewModels.Blog destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            destination.Id = source.Id;
            destination.BlogUrl = source.BlogUrl;
            destination.Name = source.Name;
            destination.IsDeleted = source.IsDeleted;
            destination.Url = _urlHelper.AbsoluteRouteUrl(BlogsControllerRoute.GetBlog, new {source.Id});
        }
    }
}