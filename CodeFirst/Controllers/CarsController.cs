namespace CodeFirst.Controllers
{
    using System.Threading;
    using System.Threading.Tasks;
    using CodeFirst.Commands;
    using CodeFirst.Constants;
    using CodeFirst.ViewModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.Net.Http.Headers;
    using Swashbuckle.AspNetCore.Annotations;

    [Route("[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CarsController : ControllerBase
    {
        /// <summary>
        /// Returns an Allow HTTP header with the allowed HTTP methods.
        /// </summary>
        /// <returns>A 200 OK response.</returns>
        [HttpOptions]
        [SwaggerResponse(StatusCodes.Status200OK, "The allowed HTTP methods.")]
        public IActionResult Options()
        {
            this.HttpContext.Response.Headers.AppendCommaSeparatedValues(
                HeaderNames.Allow,
                HttpMethods.Get,
                HttpMethods.Head,
                HttpMethods.Options,
                HttpMethods.Post);
            return this.Ok();
        }

        /// <summary>
        /// Returns an Allow HTTP header with the allowed HTTP methods for a car with the specified unique identifier.
        /// </summary>
        /// <param name="id">The cars unique identifier.</param>
        /// <returns>A 200 OK response.</returns>
        [HttpOptions("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "The allowed HTTP methods.")]
        public IActionResult Options(int id)
        {
            this.HttpContext.Response.Headers.AppendCommaSeparatedValues(
                HeaderNames.Allow,
                HttpMethods.Delete,
                HttpMethods.Get,
                HttpMethods.Head,
                HttpMethods.Options,
                HttpMethods.Patch,
                HttpMethods.Post,
                HttpMethods.Put);
            return this.Ok();
        }

        /// <summary>
        /// Deletes the car with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="id">The cars unique identifier.</param>
        /// <returns>A 204 No Content response if the car was deleted or a 404 Not Found if a car with the specified
        /// unique identifier was not found.</returns>
        [HttpDelete("{id}", Name = CarsControllerRoute.DeleteCar)]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The car with the specified unique identifier was deleted.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A car with the specified unique identifier was not found.")]
        public IActionResult Delete(
            [FromServices] IDeleteCarCommand command,
            int id) => command.Execute(id);

        /// <summary>
        /// Gets the car with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="id">The cars unique identifier.</param>
        /// <returns>A 200 OK response containing the car or a 404 Not Found if a car with the specified unique
        /// identifier was not found.</returns>
        [HttpGet("{id}", Name = CarsControllerRoute.GetCar)]
        [HttpHead("{id}", Name = CarsControllerRoute.HeadCar)]
        [SwaggerResponse(StatusCodes.Status200OK, "The car with the specified unique identifier.", typeof(Car))]
        [SwaggerResponse(StatusCodes.Status304NotModified, "The car has not changed since the date given in the If-Modified-Since HTTP header.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A car with the specified unique identifier could not be found.")]
        public IActionResult Get(
            [FromServices] IGetCarCommand command,
            int id) => command.Execute(id);

        /// <summary>
        /// Gets a collection of cars using the specified page number and number of items per page.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="pageOptions">The page options.</param>
        /// <returns>A 200 OK response containing a collection of cars, a 400 Bad Request if the page request
        /// parameters are invalid or a 404 Not Found if a page with the specified page number was not found.
        /// </returns>
        [HttpGet("", Name = CarsControllerRoute.GetCarPage)]
        [HttpHead("", Name = CarsControllerRoute.HeadCarPage)]
        [SwaggerResponse(StatusCodes.Status200OK, "A collection of cars for the specified page.", typeof(PageResult<Car>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The page request parameters are invalid.", typeof(ModelStateDictionary))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A page with the specified page number was not found.")]
        public IActionResult GetPage(
            [FromServices] IGetCarPageCommand command,
            [FromQuery] PageOptions pageOptions) => command.Execute(pageOptions);

        /// <summary>
        /// Patches the car with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="id">The cars unique identifier.</param>
        /// <param name="patch">The patch document. See http://jsonpatch.com.</param>
        /// <returns>A 200 OK if the car was patched, a 400 Bad Request if the patch was invalid or a 404 Not Found
        /// if a car with the specified unique identifier was not found.</returns>
        [HttpPatch("{id}", Name = CarsControllerRoute.PatchCar)]
        [SwaggerResponse(StatusCodes.Status200OK, "The patched car with the specified unique identifier.", typeof(Car))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The patch document is invalid.", typeof(ModelStateDictionary))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A car with the specified unique identifier could not be found.")]
        public IActionResult Patch(
            [FromServices] IPatchCarCommand command,
            int id,
            [FromBody] JsonPatchDocument<SaveCar> patch) => command.Execute(id, patch);

        /// <summary>
        /// Creates a new car.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="car">The car to create.</param>
        /// <returns>A 201 Created response containing the newly created car or a 400 Bad Request if the car is
        /// invalid.</returns>
        [HttpPost("", Name = CarsControllerRoute.PostCar)]
        [SwaggerResponse(StatusCodes.Status201Created, "The car was created.", typeof(Car))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The car is invalid.", typeof(ModelStateDictionary))]
        public IActionResult Post(
            [FromServices] IPostCarCommand command,
            [FromBody] SaveCar car) => command.Execute(car);

        /// <summary>
        /// Updates an existing car with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="id">The car identifier.</param>
        /// <param name="car">The car to update.</param>
        /// <returns>A 200 OK response containing the newly updated car, a 400 Bad Request if the car is invalid or a
        /// or a 404 Not Found if a car with the specified unique identifier was not found.</returns>
        [HttpPut("{id}", Name = CarsControllerRoute.PutCar)]
        [SwaggerResponse(StatusCodes.Status200OK, "The car was updated.", typeof(Car))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The car is invalid.", typeof(ModelStateDictionary))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A car with the specified unique identifier could not be found.")]
        public IActionResult Put(
            [FromServices] IPutCarCommand command,
            int id,
            [FromBody] SaveCar car) => command.Execute(id, car);
    }
}
