using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AccountRepository.Models
{
    public class Account
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Iban")]
        public string Iban { get; set; }

        [JsonPropertyName("Currency")]
        public string Currency { get; set; }

        [JsonPropertyName("Balance")]
        public decimal Balance { get; set; }

        [JsonPropertyName("CustomerId")]
        public int CustomerId { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

    }
}
