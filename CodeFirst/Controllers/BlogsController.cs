using CodeFirst.Commands;
using CodeFirst.Constants;
using CodeFirst.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Net.Http.Headers;
using Swashbuckle.AspNetCore.Annotations;

namespace CodeFirst.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class BlogsController : ControllerBase
    {
        /// <summary>
        /// Returns an Allow HTTP header with the allowed HTTP methods.
        /// </summary>
        /// <returns>A 200 OK response.</returns>
        [HttpOptions]
        [SwaggerResponse(StatusCodes.Status200OK, "The allowed HTTP methods.")]
        public IActionResult Options()
        {
            HttpContext.Response.Headers.AppendCommaSeparatedValues(
                HeaderNames.Allow,
                HttpMethods.Get,
                HttpMethods.Head,
                HttpMethods.Options,
                HttpMethods.Post);
            return Ok();
        }

        /// <summary>
        /// Returns an Allow HTTP header with the allowed HTTP methods for a blog with the specified unique identifier.
        /// </summary>
        /// <param name="id">The blogs unique identifier.</param>
        /// <returns>A 200 OK response.</returns>
        [HttpOptions("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "The allowed HTTP methods.")]
        public IActionResult Options(int id)
        {
            HttpContext.Response.Headers.AppendCommaSeparatedValues(
                HeaderNames.Allow,
                HttpMethods.Delete,
                HttpMethods.Get,
                HttpMethods.Head,
                HttpMethods.Options,
                HttpMethods.Patch,
                HttpMethods.Post,
                HttpMethods.Put);
            return Ok();
        }

        /// <summary>
        /// Deletes the blog with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="id">The blogs unique identifier.</param>
        /// <returns>A 204 No Content response if the blog was deleted or a 404 Not Found if a blog with the specified
        /// unique identifier was not found.</returns>
        [HttpDelete("{id}", Name = BlogsControllerRoute.DeleteBlog)]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The blog with the specified unique identifier was deleted.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A blog with the specified unique identifier was not found.")]
        public IActionResult Delete(
            [FromServices] IDeleteBlogCommand command,
            int id) => command.Execute(id);

        /// <summary>
        /// Gets the blog with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="id">The blogs unique identifier.</param>
        /// <returns>A 200 OK response containing the blog or a 404 Not Found if a blog with the specified unique
        /// identifier was not found.</returns>
        [HttpGet("{id}", Name = BlogsControllerRoute.GetBlog)]
        [HttpHead("{id}", Name = BlogsControllerRoute.HeadBlog)]
        [SwaggerResponse(StatusCodes.Status200OK, "The blog with the specified unique identifier.", typeof(Blog))]
        [SwaggerResponse(StatusCodes.Status304NotModified, "The blog has not changed since the date given in the If-Modified-Since HTTP header.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A blog with the specified unique identifier could not be found.")]
        public IActionResult Get(
            [FromServices] IGetBlogCommand command,
            int id) => command.Execute(id);

        /// <summary>
        /// Gets a collection of blogs using the specified page number and number of items per page.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="pageOptions">The page options.</param>
        /// <returns>A 200 OK response containing a collection of blogs, a 400 Bad Request if the page request
        /// parameters are invalid or a 404 Not Found if a page with the specified page number was not found.
        /// </returns>
        [HttpGet("", Name = BlogsControllerRoute.GetBlogPage)]
        [HttpHead("", Name = BlogsControllerRoute.HeadBlogPage)]
        [SwaggerResponse(StatusCodes.Status200OK, "A collection of blogs for the specified page.", typeof(PageResult<Blog>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The page request parameters are invalid.", typeof(ModelStateDictionary))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A page with the specified page number was not found.")]
        public IActionResult GetPage(
            [FromServices] IGetBlogPageCommand command,
            [FromQuery] PageOptions pageOptions) => command.Execute(pageOptions);

        /// <summary>
        /// Patches the blog with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="id">The blogs unique identifier.</param>
        /// <param name="patch">The patch document. See http://jsonpatch.com.</param>
        /// <returns>A 200 OK if the blog was patched, a 400 Bad Request if the patch was invalid or a 404 Not Found
        /// if a blog with the specified unique identifier was not found.</returns>
        [HttpPatch("{id}", Name = BlogsControllerRoute.PatchBlog)]
        [SwaggerResponse(StatusCodes.Status200OK, "The patched blog with the specified unique identifier.", typeof(Blog))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The patch document is invalid.", typeof(ModelStateDictionary))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A blog with the specified unique identifier could not be found.")]
        public IActionResult Patch(
            [FromServices] IPatchBlogCommand command,
            int id,
            [FromBody] JsonPatchDocument<SaveBlog> patch) => command.Execute(id, patch);

        /// <summary>
        /// Creates a new blog.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="blog">The blog to create.</param>
        /// <returns>A 201 Created response containing the newly created blog or a 400 Bad Request if the blog is
        /// invalid.</returns>
        [HttpPost("", Name = BlogsControllerRoute.PostBlog)]
        [SwaggerResponse(StatusCodes.Status201Created, "The blog was created.", typeof(Blog))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The blog is invalid.", typeof(ModelStateDictionary))]
        public IActionResult Post(
            [FromServices] IPostBlogCommand command,
            [FromBody] SaveBlog blog) => command.Execute(blog);

        /// <summary>
        /// Updates an existing blog with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="id">The blog identifier.</param>
        /// <param name="blog">The blog to update.</param>
        /// <returns>A 200 OK response containing the newly updated blog, a 400 Bad Request if the blog is invalid or a
        /// or a 404 Not Found if a blog with the specified unique identifier was not found.</returns>
        [HttpPut("{id}", Name = BlogsControllerRoute.PutBlog)]
        [SwaggerResponse(StatusCodes.Status200OK, "The blog was updated.", typeof(Blog))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The blog is invalid.", typeof(ModelStateDictionary))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A blog with the specified unique identifier could not be found.")]
        public IActionResult Put(
            [FromServices] IPutBlogCommand command,
            int id,
            [FromBody] SaveBlog blog) => command.Execute(id, blog);
    }
}