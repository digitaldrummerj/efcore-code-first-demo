using CodeFirst.ViewModels;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CodeFirst.ViewModelSchemaFilters
{
    public class BlogSchemaFilter : ISchemaFilter
    {
        public void Apply(Schema model, SchemaFilterContext context)
        {
            var blog = new Blog
            {
                Id = 1,
                BlogUrl = "http://localhost:1313",
                IsDeleted = false,
                Name = "Test Blog"
            };
            model.Default = blog;
            model.Example = blog;
        }
    }
}
