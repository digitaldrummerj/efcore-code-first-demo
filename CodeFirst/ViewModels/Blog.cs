using CodeFirst.ViewModelSchemaFilters;
using Swashbuckle.AspNetCore.Annotations;

namespace CodeFirst.ViewModels
{

    [SwaggerSchemaFilter(typeof(BlogSchemaFilter))]

    public class Blog : ViewModelBase
    {
        public string BlogUrl { get; set; }
        public string Name { get; set; }
    }
}