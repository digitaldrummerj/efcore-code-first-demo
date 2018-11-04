namespace CodeFirst.Commands
{
    using CodeFirst.ViewModels;
    using Boxed.AspNetCore;

    public interface IPostCarCommand : ICommand<SaveCar>
    {
    }
}
