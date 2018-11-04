namespace CodeFirst.ViewModels
{
    using CodeFirst.ViewModelSchemaFilters;
    using Swashbuckle.AspNetCore.Annotations;

    /// <summary>
    /// A make and model of car.
    /// </summary>
    [SwaggerSchemaFilter(typeof(CarSchemaFilter))]
    public class Car
    {
        /// <summary>
        /// The number of cylinders in the cars engine.
        /// </summary>
        public int Cylinders { get; set; }

        /// <summary>
        /// The make of the car.
        /// </summary>
        public string Make { get; set; }

        /// <summary>
        /// The model of the car.
        /// </summary>
        public string Model { get; set; }

        public string Url { get; set; }

        public int Id { get; set; }
    }
}
