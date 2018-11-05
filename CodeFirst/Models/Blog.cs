using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CodeFirst.Repositories;

namespace CodeFirst.Models
{
    [Table("Blogs")]
    public class Blog : ModelBase
    {
        [MaxLength(255, ErrorMessage = "Max length is 255 characters")]
        [Required]
        public string BlogUrl { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is a Required Field")]
        [MaxLength(100, ErrorMessage = "Max length is 100 characters")]
        public string Name { get; set; }
    }
}