namespace CodeFirst.Commands
{
    using System.Threading;
    using System.Threading.Tasks;
    using CodeFirst.Repositories;
    using CodeFirst.ViewModels;
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;

    public class PutCarCommand : IPutCarCommand
    {
        private readonly ICarRepository carRepository;
        private readonly IMapper<Models.Car, Car> carToCarMapper;
        private readonly IMapper<SaveCar, Models.Car> saveCarToCarMapper;

        public PutCarCommand(
            ICarRepository carRepository,
            IMapper<Models.Car, Car> carToCarMapper,
            IMapper<SaveCar, Models.Car> saveCarToCarMapper)
        {
            this.carRepository = carRepository;
            this.carToCarMapper = carToCarMapper;
            this.saveCarToCarMapper = saveCarToCarMapper;
        }

        public  IActionResult Execute(int id, SaveCar saveCar)
        {
            var car =  this.carRepository.Get(id);
            if (car == null)
            {
                return new NotFoundResult();
            }

            this.saveCarToCarMapper.Map(saveCar, car);
            this.carRepository.Update(car);
            var carViewModel = this.carToCarMapper.Map(car);

            return new OkObjectResult(carViewModel);
        }
    }
}
