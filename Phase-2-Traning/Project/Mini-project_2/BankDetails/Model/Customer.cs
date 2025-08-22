using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankDetails.Models
{
    public class Customer
    {
         public int CustomerId { get; set; }

        [Required]
        public string? Name { get; set; }

        public int Age { get; set; }

       
       // public List<BankAccount> ? BankAccounts { get; set; }
    }
}
