using System;
using Boxed.Mapping;
using CodeFirst.Models;
using CodeFirst.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Net.Http.Headers;

namespace CodeFirst.Commands
{
    public class GetCarCommand : IGetCarCommand
    {
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly ICarRepository _carRepository;
        private readonly IMapper<Car, ViewModels.Car> _carMapper;

        public GetCarCommand(
            IActionContextAccessor actionContextAccessor,
            ICarRepository carRepository,
            IMapper<Car, ViewModels.Car> carMapper)
        {
            _actionContextAccessor = actionContextAccessor;
            _carRepository = carRepository;
            _carMapper = carMapper;
        }

        public IActionResult Execute(int id)
        {
            var car = _carRepository.Get(id);
            if (car == null)
            {
                return new NotFoundResult();
            }

            var httpContext = _actionContextAccessor.ActionContext.HttpContext;
            if (httpContext.Request.Headers.TryGetValue(HeaderNames.IfModifiedSince, out var stringValues))
            {
                if (DateTimeOffset.TryParse(stringValues, out var modifiedSince) &&
                    (modifiedSince >= car.Modified))
                {
                    return new StatusCodeResult(StatusCodes.Status304NotModified);
                }
            }

            var carViewModel = _carMapper.Map(car);
            httpContext.Response.Headers.Add(HeaderNames.LastModified, car.Modified.ToString("R"));
            return new OkObjectResult(carViewModel);
        }
    }
}
