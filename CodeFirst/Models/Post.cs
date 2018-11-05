using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CodeFirst.Repositories;

namespace CodeFirst.Models
{
    [Table("Posts")]
    public class Post : ModelBase
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Title is a required field")]
        [MaxLength(200, ErrorMessage = "The maximum size of the title is 200 characters")]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}