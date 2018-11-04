namespace CodeFirst.Mappers
{
    using System;
    using CodeFirst.Services;
    using CodeFirst.ViewModels;
    using Boxed.Mapping;

    public class BlogToSaveBlogMapper : IMapper<Models.Blog, SaveBlog>, IMapper<SaveBlog, Models.Blog>
    {
        private readonly IClockService clockService;

        public BlogToSaveBlogMapper(IClockService clockService) =>
            this.clockService = clockService;

        public void Map(Models.Blog source, SaveBlog destination)
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

        public void Map(SaveBlog source, Models.Blog destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            var now = this.clockService.UtcNow;

            if (destination.Created == DateTimeOffset.MinValue)
            {
                destination.Created = now;
            }

            destination.BlogUrl = source.BlogUrl;
            destination.Name = source.Name;
            destination.Modified = now;
        }
    }
}
