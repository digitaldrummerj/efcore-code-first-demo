namespace CodeFirst.ViewModelSchemaFilters
{
    using System.Collections.Generic;
    using CodeFirst.ViewModels;
    using Swashbuckle.AspNetCore.Swagger;
    using Swashbuckle.AspNetCore.SwaggerGen;

    public class PageResultCarSchemaFilter : ISchemaFilter
    {
        public void Apply(Schema model, SchemaFilterContext context)
        {
            var pageResult = new PageResult<Car>()
            {
                Count = 2,
                Items = new List<Car>()
                {
                    new Car()
                    {
                        Id = 1,
                        Cylinders = 6,
                        Make = "Honda",
                        Model = "Civic",
                    },
                    new Car()
                    {
                        Id = 2,
                        Cylinders = 8,
                        Make = "Lambourghini",
                        Model = "Countach",
                    },
                },
                Page = 1,
                TotalCount = 50,
                TotalPages = 10,
            };
            model.Default = pageResult;
            model.Example = pageResult;
        }
    }
}
