namespace CodeFirst.ViewModels
{
    public interface IViewModelSoftDeleteBase
    {
        bool IsDeleted { get; set; }
    }
}