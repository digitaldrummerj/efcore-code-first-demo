using Boxed.Mapping;
using CodeFirst.Repositories;
using CodeFirst.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Car = CodeFirst.Models.Car;

namespace CodeFirst.Commands
{
    public class PutCarCommand : IPutCarCommand
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper<Car, ViewModels.Car> _carToCarMapper;
        private readonly IMapper<SaveCar, Car> _saveCarToCarMapper;

        public PutCarCommand(
            ICarRepository carRepository,
            IMapper<Car, ViewModels.Car> carToCarMapper,
            IMapper<SaveCar, Car> saveCarToCarMapper)
        {
            _carRepository = carRepository;
            _carToCarMapper = carToCarMapper;
            _saveCarToCarMapper = saveCarToCarMapper;
        }

        public  IActionResult Execute(int id, SaveCar saveCar)
        {
            var car =  _carRepository.Get(id);
            if (car == null)
            {
                return new NotFoundResult();
            }

            _saveCarToCarMapper.Map(saveCar, car);
            _carRepository.Update(car);
            var carViewModel = _carToCarMapper.Map(car);

            return new OkObjectResult(carViewModel);
        }
    }
}
