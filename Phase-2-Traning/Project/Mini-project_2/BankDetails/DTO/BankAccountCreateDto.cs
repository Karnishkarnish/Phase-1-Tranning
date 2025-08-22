using System;

namespace BankDetails.Dtos
{
    public class BankAccountCreateDto
    {
        public string? AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
