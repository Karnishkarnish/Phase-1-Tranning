using System;

namespace BankDetails.Dtos
{
    public class BankAccountReadDto
    {
        public int BankAccountId { get; set; }
        public string? AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
