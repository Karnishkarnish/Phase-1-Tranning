using System.ComponentModel.DataAnnotations;

namespace OrganicStore.Dtos
{
  public class OrderCreateDto
{
    [Required]
    public int CustomerId { get; set; }
    [Required]
    public int StoreId { get; set; }
    [Required]
    public string DeliveryAddress { get; set; }
    [Required]
    public List<OrderItemCreateDto> Items { get; set; } = new();
    public string Notes { get; set; }
  
}

}


