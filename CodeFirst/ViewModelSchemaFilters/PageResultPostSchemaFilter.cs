using System.Collections.Generic;
using CodeFirst.ViewModels;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CodeFirst.ViewModelSchemaFilters
{
    public class PageResultPostSchemaFilter : ISchemaFilter
    {
        public void Apply(Schema model, SchemaFilterContext context)
        {
            var pageResult = new PageResult<Post>
            {
                Count = 2,
                Items = new List<Post>
                {
                    new Post
                    {
                        Id = 1,
                        BlogId = 1,
                        Content = "Test Content",
                        Title = "Title Test",
                        Blog = new Blog
                        {
                            Id = 1,
                            BlogUrl = "http://localhost:1313",
                            Name = "Test Blog",
                            Url = "/blog/1"
                        },
                        Url = "/cars/1"
                    },
                    new Post
                    {
                        Id = 2,
                        BlogId = 1,
                        Content = "Test Content",
                        Title = "Title Test",
                        Blog = new Blog
                        {
                            Id = 1,
                            BlogUrl = "http://localhost:1313",
                            Name = "Test Blog",
                            Url = "/blog/1"
                        },
                        Url = "/cars/1"

                    }
                },
                Page = 1,
                TotalCount = 50,
                TotalPages = 10
            };
            model.Default = pageResult;
            model.Example = pageResult;
        }
    }
}
