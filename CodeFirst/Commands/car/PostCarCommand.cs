namespace CodeFirst.Commands
{
    using System.Threading;
    using System.Threading.Tasks;
    using CodeFirst.Constants;
    using CodeFirst.Repositories;
    using CodeFirst.ViewModels;
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;

    public class PostCarCommand : IPostCarCommand
    {
        private readonly ICarRepository carRepository;
        private readonly IMapper<Models.Car, Car> carToCarMapper;
        private readonly IMapper<SaveCar, Models.Car> saveCarToCarMapper;

        public PostCarCommand(
            ICarRepository carRepository,
            IMapper<Models.Car, Car> carToCarMapper,
            IMapper<SaveCar, Models.Car> saveCarToCarMapper)
        {
            this.carRepository = carRepository;
            this.carToCarMapper = carToCarMapper;
            this.saveCarToCarMapper = saveCarToCarMapper;
        }

        public  IActionResult Execute(SaveCar saveCar)
        {
            var car = this.saveCarToCarMapper.Map(saveCar);
            this.carRepository.Add(car);
            var carViewModel = this.carToCarMapper.Map(car);

            return new CreatedAtRouteResult(
                CarsControllerRoute.GetCar,
                new { id = carViewModel.Id },
                carViewModel);
        }
    }
}
