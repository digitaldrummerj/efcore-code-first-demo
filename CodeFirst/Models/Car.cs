using CodeFirst.Repositories;

namespace CodeFirst.Models
{
    public class Car : ModelBase
    {

        public int Cylinders { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }
    }
}