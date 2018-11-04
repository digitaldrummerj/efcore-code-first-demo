using Boxed.Mapping;
using CodeFirst.Repositories;
using CodeFirst.ViewModels;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Car = CodeFirst.Models.Car;

namespace CodeFirst.Commands
{
    public class PatchCarCommand : IPatchCarCommand
    {
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly IObjectModelValidator _objectModelValidator;
        private readonly ICarRepository _carRepository;
        private readonly IMapper<Car, ViewModels.Car> _carToCarMapper;
        private readonly IMapper<Car, SaveCar> _carToSaveCarMapper;
        private readonly IMapper<SaveCar, Car> _saveCarToCarMapper;

        public PatchCarCommand(
            IActionContextAccessor actionContextAccessor,
            IObjectModelValidator objectModelValidator,
            ICarRepository carRepository,
            IMapper<Car, ViewModels.Car> carToCarMapper,
            IMapper<Car, SaveCar> carToSaveCarMapper,
            IMapper<SaveCar, Car> saveCarToCarMapper)
        {
            _actionContextAccessor = actionContextAccessor;
            _objectModelValidator = objectModelValidator;
            _carRepository = carRepository;
            _carToCarMapper = carToCarMapper;
            _carToSaveCarMapper = carToSaveCarMapper;
            _saveCarToCarMapper = saveCarToCarMapper;
        }

        public IActionResult Execute(
            int id,
            JsonPatchDocument<SaveCar> patch)
        {
            var car = _carRepository.Get(id);
            if (car == null)
            {
                return new NotFoundResult();
            }

            var saveCar = _carToSaveCarMapper.Map(car);
            var modelState = _actionContextAccessor.ActionContext.ModelState;
            patch.ApplyTo(saveCar, modelState);
            _objectModelValidator.Validate(
                _actionContextAccessor.ActionContext,
                validationState: null,
                prefix: null,
                model: saveCar);
            if (!modelState.IsValid)
            {
                return new BadRequestObjectResult(modelState);
            }

            _saveCarToCarMapper.Map(saveCar, car);
            _carRepository.Update(car);
            var carViewModel = _carToCarMapper.Map(car);

            return new OkObjectResult(carViewModel);
        }
    }
}