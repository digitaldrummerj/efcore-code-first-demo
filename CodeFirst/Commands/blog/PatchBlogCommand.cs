using Boxed.AspNetCore;
using Boxed.Mapping;
using CodeFirst.Repositories;
using CodeFirst.ViewModels;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Blog = CodeFirst.Models.Blog;

namespace CodeFirst.Commands
{
    public class PatchBlogCommand : IPatchBlogCommand
    {
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly IObjectModelValidator _objectModelValidator;
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper<Blog, ViewModels.Blog> _blogToBlogMapper;
        private readonly IMapper<Blog, SaveBlog> _blogToSaveBlogMapper;
        private readonly IMapper<SaveBlog, Blog> _saveBlogToBlogMapper;

        public PatchBlogCommand(
            IActionContextAccessor actionContextAccessor,
            IObjectModelValidator objectModelValidator,
            IBlogRepository blogRepository,
            IMapper<Blog, ViewModels.Blog> blogToBlogMapper,
            IMapper<Blog, SaveBlog> blogToSaveBlogMapper,
            IMapper<SaveBlog, Blog> saveBlogToBlogMapper)
        {
            _actionContextAccessor = actionContextAccessor;
            _objectModelValidator = objectModelValidator;
            _blogRepository = blogRepository;
            _blogToBlogMapper = blogToBlogMapper;
            _blogToSaveBlogMapper = blogToSaveBlogMapper;
            _saveBlogToBlogMapper = saveBlogToBlogMapper;
        }

        public IActionResult Execute(
            int blogId,
            JsonPatchDocument<SaveBlog> patch)
        {
            var blog = _blogRepository.Get(blogId);
            if (blog == null)
            {
                return new NotFoundResult();
            }

            var saveBlog = _blogToSaveBlogMapper.Map(blog);
            var modelState = _actionContextAccessor.ActionContext.ModelState;
            patch.ApplyTo(saveBlog, modelState);
            _objectModelValidator.Validate(
                _actionContextAccessor.ActionContext,
                validationState: null,
                prefix: null,
                model: saveBlog);
            if (!modelState.IsValid)
            {
                return new BadRequestObjectResult(modelState);
            }

            _saveBlogToBlogMapper.Map(saveBlog, blog);
            _blogRepository.Update(blog);
            var blogViewModel = _blogToBlogMapper.Map(blog);

            return new OkObjectResult(blogViewModel);
        }

    }

    public interface IPatchBlogCommand : ICommand<int, JsonPatchDocument<SaveBlog>>
    {
    }
}