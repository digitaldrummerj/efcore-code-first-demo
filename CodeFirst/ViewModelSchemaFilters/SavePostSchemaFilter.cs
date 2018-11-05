using CodeFirst.ViewModels;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CodeFirst.ViewModelSchemaFilters
{
    public class SavePostSchemaFilter : ISchemaFilter
    {
        public void Apply(Schema model, SchemaFilterContext context)
        {
            var savePost = new SavePost
            {
                BlogId = 1,
                Content = "Test Content",
                Title = "Title Here",
                IsDeleted = false
            };
            model.Default = savePost;
            model.Example = savePost;
        }
    }
}
