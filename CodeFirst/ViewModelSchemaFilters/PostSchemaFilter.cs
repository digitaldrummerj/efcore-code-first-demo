using CodeFirst.ViewModels;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CodeFirst.ViewModelSchemaFilters
{
    public class PostSchemaFilter : ISchemaFilter
    {
        public void Apply(Schema model, SchemaFilterContext context)
        {
            var post = new Post
            {
                Id = 1,
                BlogId = 1,
                Content = "Test Content",
                Title = "Title Test",
                IsDeleted = false,
                Blog = new Blog
                {
                    Id = 1,
                    BlogUrl = "http://localhost:1313",
                    Name = "Test Blog",
                    IsDeleted = false
                }
            };
            model.Default = post;
            model.Example = post;
        }
    }
}
