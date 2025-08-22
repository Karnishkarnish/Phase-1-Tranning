using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BankDetails.Models
{
    public class BankAccount
    {
        public int BankAccountId { get; set; }

        [Required]
        public string? AccountNumber { get; set; }

        public decimal Amount { get; set; }

        public DateTime CreateDate { get; set; }

      
        public int CustomerId { get; set; }

        [JsonIgnore]
        public Customer? Customer { get; set; }
    }
}
