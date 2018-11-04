using System;
using Boxed.AspNetCore;
using Boxed.Mapping;
using CodeFirst.Constants;
using CodeFirst.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirst.Mappers
{
    public class CarToCarMapper : IMapper<Car, ViewModels.Car>
    {
        private readonly IUrlHelper _urlHelper;

        public CarToCarMapper(IUrlHelper urlHelper) => _urlHelper = urlHelper;

        public void Map(Car source, ViewModels.Car destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            destination.Id = source.Id;
            destination.Cylinders = source.Cylinders;
            destination.Make = source.Make;
            destination.Model = source.Model;
            destination.Url = _urlHelper.AbsoluteRouteUrl(CarsControllerRoute.GetCar, new { source.Id });
        }
    }
}
