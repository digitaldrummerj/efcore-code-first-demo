using CodeFirst.Repositories;

namespace CodeFirst.Models
{
    public class Post : ModelBase
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}