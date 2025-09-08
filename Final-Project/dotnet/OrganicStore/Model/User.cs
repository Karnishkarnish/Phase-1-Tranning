using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrganicStore.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }

        [Required]
        public string Role { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // ✅ FK for Store
        public int? StoreId { get; set; }   // Nullable, because only store owners will have this
        [ForeignKey("StoreId")]
        public virtual Store Store { get; set; }

        // ✅ Customer Address
        [MaxLength(250)]
        public string? Address { get; set; }

        // Navigation
        public virtual ICollection<Order> Orders { get; set; }
    }
}

