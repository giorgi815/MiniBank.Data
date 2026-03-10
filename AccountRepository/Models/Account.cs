using System;
using System.Collections.Generic;
using System.Text;

namespace AccountRepository.Models
{
    internal class Account
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public int CustomerId { get; set; }
        public decimal Balance { get; set; }
        public string AccountType { get; set; }

    }
}
