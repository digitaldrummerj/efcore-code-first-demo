using CodeFirst.Repositories;

namespace CodeFirst.Models
{
    public class Blog : ModelBase
    {
        public string BlogUrl { get; set; }
        public string Name { get; set; }
    }
}