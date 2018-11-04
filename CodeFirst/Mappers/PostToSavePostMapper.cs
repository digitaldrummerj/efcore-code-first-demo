using System;
using Boxed.Mapping;
using CodeFirst.Services;
using CodeFirst.ViewModels;
using Post = CodeFirst.Models.Post;

namespace CodeFirst.Mappers
{
    public class PostToSavePostMapper : IMapper<Post, SavePost>, IMapper<SavePost, Post>
    {
        private readonly IClockService _clockService;

        public PostToSavePostMapper(IClockService clockService) =>
            _clockService = clockService;

        public void Map(Post source, SavePost destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            destination.BlogId = source.BlogId;
            destination.Content = source.Content;
            destination.Title = source.Title;
        }

        public void Map(SavePost source, Post destination)
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

            if (destination.Created == DateTimeOffset.MinValue)
            {
                destination.Created = now;
            }


            destination.BlogId = source.BlogId;
            destination.Content = source.Content;
            destination.Title = source.Title;
            destination.Modified = now;
        }
    }
}
