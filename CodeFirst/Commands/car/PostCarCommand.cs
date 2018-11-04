using Boxed.Mapping;
using CodeFirst.Constants;
using CodeFirst.Repositories;
using CodeFirst.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Car = CodeFirst.Models.Car;

namespace CodeFirst.Commands
{
    public class PostCarCommand : IPostCarCommand
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper<Car, ViewModels.Car> _carToCarMapper;
        private readonly IMapper<SaveCar, Car> _saveCarToCarMapper;

        public PostCarCommand(
            ICarRepository carRepository,
            IMapper<Car, ViewModels.Car> carToCarMapper,
            IMapper<SaveCar, Car> saveCarToCarMapper)
        {
            _carRepository = carRepository;
            _carToCarMapper = carToCarMapper;
            _saveCarToCarMapper = saveCarToCarMapper;
        }

        public  IActionResult Execute(SaveCar saveCar)
        {
            var car = _saveCarToCarMapper.Map(saveCar);
            _carRepository.Add(car);
            var carViewModel = _carToCarMapper.Map(car);

            return new CreatedAtRouteResult(
                CarsControllerRoute.GetCar,
                new { id = carViewModel.Id },
                carViewModel);
        }
    }
}
