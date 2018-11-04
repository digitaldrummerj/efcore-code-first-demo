using Boxed.AspNetCore;
using Boxed.Mapping;
using CodeFirst.Repositories;
using CodeFirst.ViewModels;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CodeFirst.Commands
{
    public class PatchBlogCommand : IPatchBlogCommand
    {
        private readonly IActionContextAccessor actionContextAccessor;
        private readonly IObjectModelValidator objectModelValidator;
        private readonly IBlogRepository blogRepository;
        private readonly IMapper<Models.Blog, Blog> blogToBlogMapper;
        private readonly IMapper<Models.Blog, SaveBlog> blogToSaveBlogMapper;
        private readonly IMapper<SaveBlog, Models.Blog> saveBlogToBlogMapper;

        public PatchBlogCommand(
            IActionContextAccessor actionContextAccessor,
            IObjectModelValidator objectModelValidator,
            IBlogRepository blogRepository,
            IMapper<Models.Blog, Blog> blogToBlogMapper,
            IMapper<Models.Blog, SaveBlog> blogToSaveBlogMapper,
            IMapper<SaveBlog, Models.Blog> saveBlogToBlogMapper)
        {
            this.actionContextAccessor = actionContextAccessor;
            this.objectModelValidator = objectModelValidator;
            this.blogRepository = blogRepository;
            this.blogToBlogMapper = blogToBlogMapper;
            this.blogToSaveBlogMapper = blogToSaveBlogMapper;
            this.saveBlogToBlogMapper = saveBlogToBlogMapper;
        }

        public IActionResult Execute(
            int blogId,
            JsonPatchDocument<SaveBlog> patch)
        {
            var blog = this.blogRepository.Get(blogId);
            if (blog == null)
            {
                return new NotFoundResult();
            }

            var saveBlog = this.blogToSaveBlogMapper.Map(blog);
            var modelState = this.actionContextAccessor.ActionContext.ModelState;
            patch.ApplyTo(saveBlog, modelState);
            this.objectModelValidator.Validate(
                this.actionContextAccessor.ActionContext,
                validationState: null,
                prefix: null,
                model: saveBlog);
            if (!modelState.IsValid)
            {
                return new BadRequestObjectResult(modelState);
            }

            this.saveBlogToBlogMapper.Map(saveBlog, blog);
            this.blogRepository.Update(blog);
            var blogViewModel = this.blogToBlogMapper.Map(blog);

            return new OkObjectResult(blogViewModel);
        }

    }

    public interface IPatchBlogCommand : ICommand<int, JsonPatchDocument<SaveBlog>>
    {
    }
}