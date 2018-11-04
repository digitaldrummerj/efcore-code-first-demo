namespace CodeFirst.ViewModels
{
    public abstract class ViewModelBase : IViewModelBase
    {
        /// <summary>
        /// The URL used to retrieve the resource conforming to REST'ful JSON http://restfuljson.org/.
        /// </summary>
        public string Url { get; set; }

        public int Id { get; set; }
    }
}