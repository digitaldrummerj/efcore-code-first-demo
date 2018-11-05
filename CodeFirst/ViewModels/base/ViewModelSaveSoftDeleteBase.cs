namespace CodeFirst.ViewModels
{
    public class ViewModelSaveSoftDeleteBase : IViewModelSoftDeleteBase
    {
        public bool IsDeleted { get; set; }
    }
}