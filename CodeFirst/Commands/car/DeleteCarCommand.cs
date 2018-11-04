namespace CodeFirst.Commands
{
    using System.Threading;
    using System.Threading.Tasks;
    using CodeFirst.Repositories;
    using Microsoft.AspNetCore.Mvc;

    public class DeleteCarCommand : IDeleteCarCommand
    {
        private readonly ICarRepository carRepository;

        public DeleteCarCommand(ICarRepository carRepository) =>
            this.carRepository = carRepository;

        public IActionResult Execute(int id)
        {
            var car = this.carRepository.Get(id);
            if (car == null)
            {
                return new NotFoundResult();
            }

            this.carRepository.Delete(car);

            return new NoContentResult();
        }
    }
}
