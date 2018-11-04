namespace CodeFirst.ViewModelSchemaFilters
{
    using CodeFirst.ViewModels;
    using Swashbuckle.AspNetCore.Swagger;
    using Swashbuckle.AspNetCore.SwaggerGen;

    public class SaveBlogSchemaFilter : ISchemaFilter
    {
        public void Apply(Schema model, SchemaFilterContext context)
        {
            var saveBlog = new SaveBlog()
            {
                BlogUrl = "localhost:1314",
                Name = "Blog Name"
            };
            model.Default = saveBlog;
            model.Example = saveBlog;
        }
    }
}
