using Boxed.AspNetCore;
using Boxed.Mapping;
using CodeFirst.Repositories;
using CodeFirst.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirst.Commands
{
    public class PutPostCommand : IPutPostCommand
    {
        private readonly IPostRepository postRepository;
        private readonly IMapper<Models.Post, Post> postToPostMapper;
        private readonly IMapper<SavePost, Models.Post> savePostToPostMapper;

        public PutPostCommand(
            IPostRepository postRepository,
            IMapper<Models.Post, Post> postToPostMapper,
            IMapper<SavePost, Models.Post> savePostToPostMapper)
        {
            this.postRepository = postRepository;
            this.postToPostMapper = postToPostMapper;
            this.savePostToPostMapper = savePostToPostMapper;
        }

        public IActionResult Execute(int postId, SavePost savePost)
        {
            var post = this.postRepository.Get(postId);
            if (post == null)
            {
                return new NotFoundResult();
            }

            this.savePostToPostMapper.Map(savePost, post);
            this.postRepository.Update(post);
            var postViewModel = this.postToPostMapper.Map(post);

            return new OkObjectResult(postViewModel);
        }

    }

    public interface IPutPostCommand : ICommand<int, SavePost>
    {
    }
}