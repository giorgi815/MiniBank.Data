using AccountRepository.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountRepository.Models
{
    public class Operation
    {
        public int Id { get; set; }
        public OperationType operationType { get; set; }
        public int AcountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime HappendAt { get; set; } = DateTime.Now;
    }
}
