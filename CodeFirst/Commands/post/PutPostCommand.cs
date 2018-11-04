using Boxed.AspNetCore;
using Boxed.Mapping;
using CodeFirst.Repositories;
using CodeFirst.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Post = CodeFirst.Models.Post;

namespace CodeFirst.Commands
{
    public class PutPostCommand : IPutPostCommand
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper<Post, ViewModels.Post> _postToPostMapper;
        private readonly IMapper<SavePost, Post> _savePostToPostMapper;

        public PutPostCommand(
            IPostRepository postRepository,
            IMapper<Post, ViewModels.Post> postToPostMapper,
            IMapper<SavePost, Post> savePostToPostMapper)
        {
            _postRepository = postRepository;
            _postToPostMapper = postToPostMapper;
            _savePostToPostMapper = savePostToPostMapper;
        }

        public IActionResult Execute(int postId, SavePost savePost)
        {
            var post = _postRepository.Get(postId);
            if (post == null)
            {
                return new NotFoundResult();
            }

            _savePostToPostMapper.Map(savePost, post);
            _postRepository.Update(post);
            var postViewModel = _postToPostMapper.Map(post);

            return new OkObjectResult(postViewModel);
        }

    }

    public interface IPutPostCommand : ICommand<int, SavePost>
    {
    }
}