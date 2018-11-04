namespace CodeFirst.Commands
{
    using CodeFirst.ViewModels;
    using Boxed.AspNetCore;

    public interface IPutCarCommand : ICommand<int, SaveCar>
    {
    }
}
