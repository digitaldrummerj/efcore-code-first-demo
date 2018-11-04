namespace CodeFirst.Mappers
{
    using System;
    using CodeFirst.Services;
    using CodeFirst.ViewModels;
    using Boxed.Mapping;

    public class PostToSavePostMapper : IMapper<Models.Post, SavePost>, IMapper<SavePost, Models.Post>
    {
        private readonly IClockService clockService;

        public PostToSavePostMapper(IClockService clockService) =>
            this.clockService = clockService;

        public void Map(Models.Post source, SavePost destination)
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

        public void Map(SavePost source, Models.Post destination)
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


            destination.BlogId = source.BlogId;
            destination.Content = source.Content;
            destination.Title = source.Title;
            destination.Modified = now;
        }
    }
}
