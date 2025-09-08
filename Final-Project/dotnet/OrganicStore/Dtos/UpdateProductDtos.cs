using System.ComponentModel.DataAnnotations;

namespace OrganicStore.Dtos
{
    public class UpdateProductDto
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        [MaxLength(50)]
        public string? Category { get; set; }

        public string? ImageUrl { get; set; }

        public bool IsAvailable { get; set; }
    }
}
