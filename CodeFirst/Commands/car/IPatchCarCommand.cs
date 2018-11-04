using Boxed.AspNetCore;
using CodeFirst.ViewModels;
using Microsoft.AspNetCore.JsonPatch;

namespace CodeFirst.Commands
{
    public interface IPatchCarCommand : ICommand<int, JsonPatchDocument<SaveCar>>
    {
    }
}
