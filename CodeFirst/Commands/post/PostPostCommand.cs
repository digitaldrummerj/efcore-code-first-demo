using Boxed.AspNetCore;
using Boxed.Mapping;
using CodeFirst.Constants;
using CodeFirst.Repositories;
using CodeFirst.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirst.Commands
{
    public class PostPostCommand : IPostPostCommand
    {
        private readonly IPostRepository postRepository;
        private readonly IMapper<Models.Post, Post> postToPostMapper;
        private readonly IMapper<SavePost, Models.Post> savePostToPostMapper;

        public PostPostCommand(
            IPostRepository postRepository,
            IMapper<Models.Post, Post> postToPostMapper,
            IMapper<SavePost, Models.Post> savePostToPostMapper)
        {
            this.postRepository = postRepository;
            this.postToPostMapper = postToPostMapper;
            this.savePostToPostMapper = savePostToPostMapper;
        }

        public IActionResult Execute(SavePost savePost)
        {
            var post = this.savePostToPostMapper.Map(savePost);
            this.postRepository.Add(post);
            var postViewModel = this.postToPostMapper.Map(post);

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