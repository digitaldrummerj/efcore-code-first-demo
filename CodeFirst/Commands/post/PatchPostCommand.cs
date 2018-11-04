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
    public class PatchPostCommand : IPatchPostCommand
    {
        private readonly IActionContextAccessor actionContextAccessor;
        private readonly IObjectModelValidator objectModelValidator;
        private readonly IPostRepository postRepository;
        private readonly IMapper<Models.Post, Post> postToPostMapper;
        private readonly IMapper<Models.Post, SavePost> postToSavePostMapper;
        private readonly IMapper<SavePost, Models.Post> savePostToPostMapper;

        public PatchPostCommand(
            IActionContextAccessor actionContextAccessor,
            IObjectModelValidator objectModelValidator,
            IPostRepository postRepository,
            IMapper<Models.Post, Post> postToPostMapper,
            IMapper<Models.Post, SavePost> postToSavePostMapper,
            IMapper<SavePost, Models.Post> savePostToPostMapper)
        {
            this.actionContextAccessor = actionContextAccessor;
            this.objectModelValidator = objectModelValidator;
            this.postRepository = postRepository;
            this.postToPostMapper = postToPostMapper;
            this.postToSavePostMapper = postToSavePostMapper;
            this.savePostToPostMapper = savePostToPostMapper;
        }

        public IActionResult Execute(
            int postId,
            JsonPatchDocument<SavePost> patch)
        {
            var post = this.postRepository.Get(postId);
            if (post == null)
            {
                return new NotFoundResult();
            }

            var savePost = this.postToSavePostMapper.Map(post);
            var modelState = this.actionContextAccessor.ActionContext.ModelState;
            patch.ApplyTo(savePost, modelState);
            this.objectModelValidator.Validate(
                this.actionContextAccessor.ActionContext,
                validationState: null,
                prefix: null,
                model: savePost);
            if (!modelState.IsValid)
            {
                return new BadRequestObjectResult(modelState);
            }

            this.savePostToPostMapper.Map(savePost, post);
            this.postRepository.Update(post);
            var postViewModel = this.postToPostMapper.Map(post);

            return new OkObjectResult(postViewModel);
        }

    }

    public interface IPatchPostCommand : ICommand<int, JsonPatchDocument<SavePost>>
    {
    }
}