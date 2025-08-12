using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Expence
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        [Required]
        public string ? Des { get; set; }
    }
}

