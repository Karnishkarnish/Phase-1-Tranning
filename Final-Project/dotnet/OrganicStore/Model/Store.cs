using OrganicStore.Model;
using System.ComponentModel.DataAnnotations;

public class Store
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [MaxLength(500)]
    public string Description { get; set; }

    [MaxLength(200)]
    public string Address { get; set; }

    [Phone]
    public string Phone { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Foreign key
    public int OwnerId { get; set; }

    // Navigation properties - ADD THESE
    public virtual User Owner { get; set; }
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}