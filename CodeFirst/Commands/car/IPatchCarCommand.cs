namespace CodeFirst.Commands
{
    using CodeFirst.ViewModels;
    using Boxed.AspNetCore;
    using Microsoft.AspNetCore.JsonPatch;

    public interface IPatchCarCommand : ICommand<int, JsonPatchDocument<SaveCar>>
    {
    }
}
