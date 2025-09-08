using System.ComponentModel.DataAnnotations;

namespace OrganicStore.Dtos
{
    public class OrderStatusUpdateDto
    {
        [Required]
        public string Status { get; set; } // e.g., Placed, Accepted, Packing, Ready, OutForDelivery, Delivered, Cancelled
    }
}
