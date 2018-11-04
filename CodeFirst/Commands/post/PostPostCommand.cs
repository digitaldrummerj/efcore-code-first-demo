using Boxed.AspNetCore;
using Boxed.Mapping;
using CodeFirst.Constants;
using CodeFirst.Repositories;
using CodeFirst.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Post = CodeFirst.Models.Post;

namespace CodeFirst.Commands
{
    public class PostPostCommand : IPostPostCommand
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper<Post, ViewModels.Post> _postToPostMapper;
        private readonly IMapper<SavePost, Post> _savePostToPostMapper;

        public PostPostCommand(
            IPostRepository postRepository,
            IMapper<Post, ViewModels.Post> postToPostMapper,
            IMapper<SavePost, Post> savePostToPostMapper)
        {
            _postRepository = postRepository;
            _postToPostMapper = postToPostMapper;
            _savePostToPostMapper = savePostToPostMapper;
        }

        public IActionResult Execute(SavePost savePost)
        {
            var post = _savePostToPostMapper.Map(savePost);
            _postRepository.Add(post);
            var postViewModel = _postToPostMapper.Map(post);

            return new CreatedAtRouteResult(
                PostsControllerRoute.GetPost,
                new { id = postViewModel.Id },
                postViewModel);
        }
    }

    public interface IPostPostCommand : ICommand<SavePost>
    {
    }
}