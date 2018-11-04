using CodeFirst.ViewModelSchemaFilters;
using Swashbuckle.AspNetCore.Annotations;

namespace CodeFirst.ViewModels
{
    [SwaggerSchemaFilter(typeof(SaveBlogSchemaFilter))]

    public class SaveBlog
    {
        public string BlogUrl { get; set; }
        public string Name { get; set; }
    }
}