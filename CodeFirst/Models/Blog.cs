using System.ComponentModel.DataAnnotations.Schema;
using CodeFirst.Repositories;

namespace CodeFirst.Models
{
    [Table("Blogs")]
    public class Blog : ModelBase
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Blog Url is a Required Field")]
        [MaxLength(255, ErrorMessage = "Max length is 255 characters")]
        public string BlogUrl { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is a Required Field")]
        [MaxLength(100, ErrorMessage = "Max length is 100 characters")]
        public string Name { get; set; }
    }
}