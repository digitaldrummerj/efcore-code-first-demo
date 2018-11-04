using System;
using Boxed.AspNetCore;
using Boxed.Mapping;
using CodeFirst.Constants;
using CodeFirst.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirst.Mappers
{
    public class BlogToBlogMapper : IMapper<Models.Blog, Blog>
    {
        private readonly IUrlHelper urlHelper;

        public BlogToBlogMapper(IUrlHelper urlHelper) => this.urlHelper = urlHelper;

        public void Map(Models.Blog source, Blog destination)
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
            destination.Url = this.urlHelper.AbsoluteRouteUrl(BlogsControllerRoute.GetBlog, new {source.Id});
        }
    }
}