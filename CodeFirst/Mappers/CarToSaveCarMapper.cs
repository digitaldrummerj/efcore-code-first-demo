using System;
using Boxed.Mapping;
using CodeFirst.Services;
using CodeFirst.ViewModels;
using Car = CodeFirst.Models.Car;

namespace CodeFirst.Mappers
{
    public class CarToSaveCarMapper : IMapper<Car, SaveCar>, IMapper<SaveCar, Car>
    {
        private readonly IClockService _clockService;

        public CarToSaveCarMapper(IClockService clockService) =>
            _clockService = clockService;

        public void Map(Car source, SaveCar destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            destination.Cylinders = source.Cylinders;
            destination.Make = source.Make;
            destination.Model = source.Model;
        }

        public void Map(SaveCar source, Car destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            var now = _clockService.UtcNow;

            if (destination.Created == DateTimeOffset.MinValue)
            {
                destination.Created = now;
            }

            destination.Cylinders = source.Cylinders;
            destination.Make = source.Make;
            destination.Model = source.Model;
            destination.Modified = now;
        }
    }
}
