using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CodeFirst.Repositories;

namespace CodeFirst.Models
{
    [Table("Blogs")]
    public class Blog : SoftDelete
    {
        [MaxLength(255, ErrorMessage = "Max length is 255 characters")]
        [Required]
        public string BlogUrl { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is a Required Field")]
        [MaxLength(100, ErrorMessage = "Max length is 100 characters")]
        public string Name { get; set; }

        [InverseProperty(nameof(ViewModels.Post.Blog))]
        public List<Post> Posts { get; set; } = new List<Post>();
    }
}