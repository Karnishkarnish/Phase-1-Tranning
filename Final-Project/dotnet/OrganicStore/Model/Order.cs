using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrganicStore.Model
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "Pending"; // Pending, Confirmed, Processing, Shipped, Delivered, Cancelled

        [MaxLength(500)]
        public string DeliveryAddress { get; set; }

        public DateTime? DeliveryDate { get; set; }

        // Foreign keys
        public int CustomerId { get; set; }
        public int? StoreId { get; set; }

       
        public virtual User Customer { get; set; }
        public virtual Store Store { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual Payment Payment { get; set; }
    }
}
