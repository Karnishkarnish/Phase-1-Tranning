using System.ComponentModel.DataAnnotations;

namespace MiniProject.Models
{
    public class Audience
    {
        public int Id { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Name must be at least 5 characters.")]
        public string Name { get; set; } = "";

        [Required]
        public string Gender { get; set; } = "";

        [Required]
        [Range(18, 120, ErrorMessage = "Age must be above 18.")]
        public int Age { get; set; }
        [Required]
        public DateOnly Birthday { get; set; }
    }
}
