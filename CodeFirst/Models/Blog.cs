using System.ComponentModel.DataAnnotations.Schema;
using CodeFirst.Repositories;

namespace CodeFirst.Models
{
    [Table("Blogs")]
    public class Blog : ModelBase
    {
        public string BlogUrl { get; set; }
        public string Name { get; set; }
    }
}