using Boxed.AspNetCore;
using CodeFirst.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirst.Commands
{
    public class DeletePostCommand : IDeletePostCommand 
    {
        private readonly IPostRepository _postRepository;

        public DeletePostCommand(IPostRepository postRepository) =>
            _postRepository = postRepository;

        public IActionResult Execute(int id)
        {
            var car = _postRepository.Get(id);
            if (car == null)
            {
                return new NotFoundResult();
            }

            _postRepository.Delete(car);

            return new NoContentResult();
        }
    }

    public interface IDeletePostCommand : ICommand<int>
    {
    }
}