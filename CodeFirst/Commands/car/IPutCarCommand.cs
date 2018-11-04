using Boxed.AspNetCore;
using CodeFirst.ViewModels;

namespace CodeFirst.Commands
{
    public interface IPutCarCommand : ICommand<int, SaveCar>
    {
    }
}
