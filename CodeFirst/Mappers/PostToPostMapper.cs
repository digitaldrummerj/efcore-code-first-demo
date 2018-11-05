using System;
using Boxed.AspNetCore;
using Boxed.Mapping;
using CodeFirst.Constants;
using CodeFirst.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirst.Mappers
{
    public class PostToPostMapper : IMapper<Post, ViewModels.Post>
    {
    private readonly IUrlHelper _urlHelper;

    public PostToPostMapper(IUrlHelper urlHelper) => _urlHelper = urlHelper;

    public void Map(Post source, ViewModels.Post destination)
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
        if (source.Blog != null)
        {
            destination.Blog = new BlogToBlogMapper(_urlHelper).Map(source.Blog);
        }

        destination.BlogId = source.BlogId;
        destination.Content = source.Content;
        destination.Title = source.Title;
        destination.Url = _urlHelper.AbsoluteRouteUrl(PostsControllerRoute.GetPost, new { source.Id });
    }

}
}