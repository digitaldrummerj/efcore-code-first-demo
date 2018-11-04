using CodeFirst.ViewModelSchemaFilters;
using Swashbuckle.AspNetCore.Annotations;

namespace CodeFirst.ViewModels
{
    [SwaggerSchemaFilter(typeof(SavePostSchemaFilter))]

    public class SavePost
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}