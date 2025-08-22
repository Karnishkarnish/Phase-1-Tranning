using System.Collections.Generic;

namespace BankDetails.Dtos
{
    public class CustomerReadDto
    {
       /// <summary>
       //public int CustomerId { get; set; }
       /// </summary>
        public string? Name { get; set; }
        public int Age { get; set; }

        public List<BankAccountReadDto> ? BankAccounts { get; set; }
    }
}

