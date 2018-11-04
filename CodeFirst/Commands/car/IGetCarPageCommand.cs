namespace CodeFirst.Commands
{
    using CodeFirst.ViewModels;
    using Boxed.AspNetCore;

    public interface IGetCarPageCommand : ICommand<PageOptions>
    {
    }
}
