namespace CodeFirst.ViewModels
{
    public abstract class ViewModelSoftDeleteBase : ViewModelBase
    {
        public bool IsDeleted { get; set; }
    }
}