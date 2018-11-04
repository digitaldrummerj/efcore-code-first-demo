using System;
using Boxed.AspNetCore;
using Boxed.Mapping;
using CodeFirst.Constants;
using CodeFirst.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirst.Mappers
{
    public class PostToPostMapper : IMapper<Models.Post, Post>
    {
    private readonly IUrlHelper urlHelper;

    public PostToPostMapper(IUrlHelper urlHelper) => this.urlHelper = urlHelper;

    public void Map(Models.Post source, Post destination)
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
        destination.Blog = new BlogToBlogMapper(urlHelper).Map(source.Blog);
        destination.BlogId = source.BlogId;
        destination.Content = source.Content;
        destination.Title = source.Title;
        destination.Url = this.urlHelper.AbsoluteRouteUrl(PostsControllerRoute.GetPost, new { source.Id });
    }

}
}