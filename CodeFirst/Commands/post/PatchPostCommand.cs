using Boxed.AspNetCore;
using Boxed.Mapping;
using CodeFirst.Repositories;
using CodeFirst.ViewModels;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Post = CodeFirst.Models.Post;

namespace CodeFirst.Commands
{
    public class PatchPostCommand : IPatchPostCommand
    {
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly IObjectModelValidator _objectModelValidator;
        private readonly IPostRepository _postRepository;
        private readonly IMapper<Post, ViewModels.Post> _postToPostMapper;
        private readonly IMapper<Post, SavePost> _postToSavePostMapper;
        private readonly IMapper<SavePost, Post> _savePostToPostMapper;

        public PatchPostCommand(
            IActionContextAccessor actionContextAccessor,
            IObjectModelValidator objectModelValidator,
            IPostRepository postRepository,
            IMapper<Post, ViewModels.Post> postToPostMapper,
            IMapper<Post, SavePost> postToSavePostMapper,
            IMapper<SavePost, Post> savePostToPostMapper)
        {
            _actionContextAccessor = actionContextAccessor;
            _objectModelValidator = objectModelValidator;
            _postRepository = postRepository;
            _postToPostMapper = postToPostMapper;
            _postToSavePostMapper = postToSavePostMapper;
            _savePostToPostMapper = savePostToPostMapper;
        }

        public IActionResult Execute(
            int postId,
            JsonPatchDocument<SavePost> patch)
        {
            var post = _postRepository.Get(postId);
            if (post == null)
            {
                return new NotFoundResult();
            }

            var savePost = _postToSavePostMapper.Map(post);
            var modelState = _actionContextAccessor.ActionContext.ModelState;
            patch.ApplyTo(savePost, modelState);
            _objectModelValidator.Validate(
                _actionContextAccessor.ActionContext,
                validationState: null,
                prefix: null,
                model: savePost);
            if (!modelState.IsValid)
            {
                return new BadRequestObjectResult(modelState);
            }

            _savePostToPostMapper.Map(savePost, post);
            _postRepository.Update(post);
            var postViewModel = _postToPostMapper.Map(post);

            return new OkObjectResult(postViewModel);
        }

    }

    public interface IPatchPostCommand : ICommand<int, JsonPatchDocument<SavePost>>
    {
    }
}