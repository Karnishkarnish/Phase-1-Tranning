using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;  

namespace OrganicStore.Model
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Column(TypeName = "decimal(18,2)")]  
        public decimal Amount { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        [Required]
        public string PaymentMethod { get; set; }

       
        public Order Order { get; set; }
    }
}
