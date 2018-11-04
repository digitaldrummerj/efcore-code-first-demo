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
    public class PostsController : ControllerBase
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
        /// Returns an Allow HTTP header with the allowed HTTP methods for a post with the specified unique identifier.
        /// </summary>
        /// <param name="id">The posts unique identifier.</param>
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
        /// Deletes the post with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="id">The posts unique identifier.</param>
        /// <returns>A 204 No Content response if the post was deleted or a 404 Not Found if a post with the specified
        /// unique identifier was not found.</returns>
        [HttpDelete("{id}", Name = PostsControllerRoute.DeletePost)]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The post with the specified unique identifier was deleted.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A post with the specified unique identifier was not found.")]
        public IActionResult Delete(
            [FromServices] IDeletePostCommand command,
            int id) => command.Execute(id);

        /// <summary>
        /// Gets the post with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="id">The posts unique identifier.</param>
        /// <returns>A 200 OK response containing the post or a 404 Not Found if a post with the specified unique
        /// identifier was not found.</returns>
        [HttpGet("{id}", Name = PostsControllerRoute.GetPost)]
        [HttpHead("{id}", Name = PostsControllerRoute.HeadPost)]
        [SwaggerResponse(StatusCodes.Status200OK, "The post with the specified unique identifier.", typeof(Post))]
        [SwaggerResponse(StatusCodes.Status304NotModified, "The post has not changed since the date given in the If-Modified-Since HTTP header.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A post with the specified unique identifier could not be found.")]
        public IActionResult Get(
            [FromServices] IGetPostCommand command,
            int id) => command.Execute(id);

        /// <summary>
        /// Gets a collection of posts using the specified page number and number of items per page.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="pageOptions">The page options.</param>
        /// <returns>A 200 OK response containing a collection of posts, a 400 Bad Request if the page request
        /// parameters are invalid or a 404 Not Found if a page with the specified page number was not found.
        /// </returns>
        [HttpGet("", Name = PostsControllerRoute.GetPostPage)]
        [HttpHead("", Name = PostsControllerRoute.HeadPostPage)]
        [SwaggerResponse(StatusCodes.Status200OK, "A collection of posts for the specified page.", typeof(PageResult<Post>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The page request parameters are invalid.", typeof(ModelStateDictionary))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A page with the specified page number was not found.")]
        public IActionResult GetPage(
            [FromServices] IGetPostPageCommand command,
            [FromQuery] PageOptions pageOptions) => command.Execute(pageOptions);

        /// <summary>
        /// Patches the post with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="id">The posts unique identifier.</param>
        /// <param name="patch">The patch document. See http://jsonpatch.com.</param>
        /// <returns>A 200 OK if the post was patched, a 400 Bad Request if the patch was invalid or a 404 Not Found
        /// if a post with the specified unique identifier was not found.</returns>
        [HttpPatch("{id}", Name = PostsControllerRoute.PatchPost)]
        [SwaggerResponse(StatusCodes.Status200OK, "The patched post with the specified unique identifier.", typeof(Post))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The patch document is invalid.", typeof(ModelStateDictionary))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A post with the specified unique identifier could not be found.")]
        public IActionResult Patch(
            [FromServices] IPatchPostCommand command,
            int id,
            [FromBody] JsonPatchDocument<SavePost> patch) => command.Execute(id, patch);

        /// <summary>
        /// Creates a new post.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="post">The post to create.</param>
        /// <returns>A 201 Created response containing the newly created post or a 400 Bad Request if the post is
        /// invalid.</returns>
        [HttpPost("", Name = PostsControllerRoute.PostPost)]
        [SwaggerResponse(StatusCodes.Status201Created, "The post was created.", typeof(Post))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The post is invalid.", typeof(ModelStateDictionary))]
        public IActionResult Post(
            [FromServices] IPostPostCommand command,
            [FromBody] SavePost post) => command.Execute(post);

        /// <summary>
        /// Updates an existing post with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="id">The post identifier.</param>
        /// <param name="post">The post to update.</param>
        /// <returns>A 200 OK response containing the newly updated post, a 400 Bad Request if the post is invalid or a
        /// or a 404 Not Found if a post with the specified unique identifier was not found.</returns>
        [HttpPut("{id}", Name = PostsControllerRoute.PutPost)]
        [SwaggerResponse(StatusCodes.Status200OK, "The post was updated.", typeof(Post))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The post is invalid.", typeof(ModelStateDictionary))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A post with the specified unique identifier could not be found.")]
        public IActionResult Put(
            [FromServices] IPutPostCommand command,
            int id,
            [FromBody] SavePost post) => command.Execute(id, post);
    }
}