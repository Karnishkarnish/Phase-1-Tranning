using System.ComponentModel.DataAnnotations;

namespace Day6Web.Models
{
    public class MyTree
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string? Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 10)]
        public string? Description { get; set; }


        [Required]
        [Range(1, 50)]
        public int TreeCount { get; set; }

    }
}
