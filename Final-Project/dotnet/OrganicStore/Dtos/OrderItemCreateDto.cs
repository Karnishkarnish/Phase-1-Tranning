using System.ComponentModel.DataAnnotations;

namespace OrganicStore.Dtos
{
public class OrderItemCreateDto
{
    [Required]
    public int ProductId { get; set; }
    [Required]
    public int Quantity { get; set; }
}

}
