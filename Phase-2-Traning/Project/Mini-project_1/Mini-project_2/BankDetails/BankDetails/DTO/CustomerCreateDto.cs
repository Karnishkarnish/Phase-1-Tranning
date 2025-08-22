using System.Collections.Generic;

namespace BankDetails.Dtos
{
    public class CustomerCreateDto
    {
        public string Name { get; set; }
        public int Age { get; set; }

        
        public List<BankAccountCreateDto> ? BankAccounts { get; set; }
    }
}

