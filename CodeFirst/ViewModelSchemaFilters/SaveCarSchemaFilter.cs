using CodeFirst.ViewModels;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CodeFirst.ViewModelSchemaFilters
{
    public class SaveCarSchemaFilter : ISchemaFilter
    {
        public void Apply(Schema model, SchemaFilterContext context)
        {
            var saveCar = new SaveCar
            {
                Cylinders = 6,
                Make = "Honda",
                Model = "Civic"
            };
            model.Default = saveCar;
            model.Example = saveCar;
        }
    }
}
