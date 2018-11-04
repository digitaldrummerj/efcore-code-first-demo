using CodeFirst.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirst.Commands
{
    public class DeleteCarCommand : IDeleteCarCommand
    {
        private readonly ICarRepository _carRepository;

        public DeleteCarCommand(ICarRepository carRepository) =>
            _carRepository = carRepository;

        public IActionResult Execute(int id)
        {
            var car = _carRepository.Get(id);
            if (car == null)
            {
                return new NotFoundResult();
            }

            _carRepository.Delete(car);

            return new NoContentResult();
        }
    }
}
