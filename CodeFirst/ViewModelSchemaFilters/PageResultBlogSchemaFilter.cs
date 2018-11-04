using System.Collections.Generic;
using CodeFirst.ViewModels;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CodeFirst.ViewModelSchemaFilters
{
    public class PageResultBlogSchemaFilter : ISchemaFilter
    {
        public void Apply(Schema model, SchemaFilterContext context)
        {
            var pageResult = new PageResult<Blog>
            {
                Count = 2,
                Items = new List<Blog>
                {
                    new Blog
                    {
                        Id = 1,
                        BlogUrl = "localhost:1313",
                        Name = "Test 1"
                    },
                    new Blog
                    {
                        Id = 2,
                        BlogUrl = "localhost:5000",
                        Name = "Test 2"
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
