using Boxed.Mapping;
using CodeFirst.Commands;
using CodeFirst.Mappers;
using CodeFirst.Repositories;
using CodeFirst.Services;
using CodeFirst.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Blog = CodeFirst.Models.Blog;
using Car = CodeFirst.Models.Car;
using Post = CodeFirst.Models.Post;

namespace CodeFirst
{
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
                .AddSingleton<IMapper<Car, ViewModels.Car>, CarToCarMapper>()
                .AddSingleton<IMapper<Car, SaveCar>, CarToSaveCarMapper>()
                .AddSingleton<IMapper<SaveCar, Car>, CarToSaveCarMapper>()

                .AddSingleton<IMapper<Blog, ViewModels.Blog>, BlogToBlogMapper>()
                .AddSingleton<IMapper<Blog, SaveBlog>, BlogToSaveBlogMapper>()
                .AddSingleton<IMapper<SaveBlog, Blog>, BlogToSaveBlogMapper>()
                
                .AddSingleton<IMapper<Post, ViewModels.Post>, PostToPostMapper>()
                .AddSingleton<IMapper<Post, SavePost>, PostToSavePostMapper>()
                .AddSingleton<IMapper<SavePost, Post>, PostToSavePostMapper>()

        ;

        public static IServiceCollection AddProjectRepositories(this IServiceCollection services) =>
            services
                .AddScoped<ICarRepository, CarRepository>()
                .AddScoped<IBlogRepository, BlogRepository>()
                .AddScoped<IPostRepository, PostRepository>();

        public static IServiceCollection AddProjectServices(this IServiceCollection services) =>
            services
                .AddSingleton<IClockService, ClockService>();
    }
}
