namespace CodeFirst
{
    using CodeFirst.Commands;
    using CodeFirst.Mappers;
    using CodeFirst.Repositories;
    using CodeFirst.Services;
    using CodeFirst.ViewModels;
    using Boxed.Mapping;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods add project services.
    /// </summary>
    /// <remarks>
    /// AddSingleton - Only one instance is ever created and returned.
    /// AddScoped - A new instance is created and returned for each request/response cycle.
    /// AddTransient - A new instance is created and returned each time.
    /// </remarks>
    public static class ProjectServiceCollectionExtensions
    {
        public static IServiceCollection AddProjectCommands(this IServiceCollection services) =>
            services
                .AddSingleton<IDeleteCarCommand, DeleteCarCommand>()
                .AddSingleton<IGetCarCommand, GetCarCommand>()
                .AddSingleton<IGetCarPageCommand, GetCarPageCommand>()
                .AddSingleton<IPatchCarCommand, PatchCarCommand>()
                .AddSingleton<IPostCarCommand, PostCarCommand>()
                .AddSingleton<IPutCarCommand, PutCarCommand>()

                .AddSingleton<IDeleteBlogCommand, DeleteBlogCommand>()
                .AddSingleton<IGetBlogCommand, GetBlogCommand>()
                .AddSingleton<IGetBlogPageCommand, GetBlogPageCommand>()
                .AddSingleton<IPatchBlogCommand, PatchBlogCommand>()
                .AddSingleton<IPostBlogCommand, PostBlogCommand>()
                .AddSingleton<IPutBlogCommand, PutBlogCommand>()
                
                .AddSingleton<IDeletePostCommand, DeletePostCommand>()
                .AddSingleton<IGetPostCommand, GetPostCommand>()
                .AddSingleton<IGetPostPageCommand, GetPostPageCommand>()
                .AddSingleton<IPatchPostCommand, PatchPostCommand>()
                .AddSingleton<IPostPostCommand, PostPostCommand>()
                .AddSingleton<IPutPostCommand, PutPostCommand>();

        public static IServiceCollection AddProjectMappers(this IServiceCollection services) =>
            services
                .AddSingleton<IMapper<Models.Car, Car>, CarToCarMapper>()
                .AddSingleton<IMapper<Models.Car, SaveCar>, CarToSaveCarMapper>()
                .AddSingleton<IMapper<SaveCar, Models.Car>, CarToSaveCarMapper>()

                .AddSingleton<IMapper<Models.Blog, Blog>, BlogToBlogMapper>()
                .AddSingleton<IMapper<Models.Blog, SaveBlog>, BlogToSaveBlogMapper>()
                .AddSingleton<IMapper<SaveBlog, Models.Blog>, BlogToSaveBlogMapper>()
                
                .AddSingleton<IMapper<Models.Post, Post>, PostToPostMapper>()
                .AddSingleton<IMapper<Models.Post, SavePost>, PostToSavePostMapper>()
                .AddSingleton<IMapper<SavePost, Models.Post>, PostToSavePostMapper>()

        ;

        public static IServiceCollection AddProjectRepositories(this IServiceCollection services) =>
            services
                .AddSingleton<ICarRepository, CarRepository>()
                .AddSingleton<IBlogRepository, BlogRepository>()
                .AddSingleton<IPostRepository, PostRepository>();

        public static IServiceCollection AddProjectServices(this IServiceCollection services) =>
            services
                .AddSingleton<IClockService, ClockService>();
    }
}
