using System;
using Boxed.Mapping;
using CodeFirst.Services;
using CodeFirst.ViewModels;
using Blog = CodeFirst.Models.Blog;

namespace CodeFirst.Mappers
{
    public class BlogToSaveBlogMapper : IMapper<Blog, SaveBlog>, IMapper<SaveBlog, Blog>
    {
        private readonly IClockService _clockService;

        public BlogToSaveBlogMapper(IClockService clockService) =>
            _clockService = clockService;

        public void Map(Blog source, SaveBlog destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            destination.BlogUrl = source.BlogUrl;
            destination.Name = source.Name;
        }

        public void Map(SaveBlog source, Blog destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            var now = _clockService.UtcNow;

            //if (destination.Created == DateTimeOffset.MinValue)
            //{
            //    destination.Created = now;
            //}

            destination.BlogUrl = source.BlogUrl;
            destination.Name = source.Name;
            destination.IsDeleted = source.IsDeleted;
            destination.Modified = now;
        }
    }
}
