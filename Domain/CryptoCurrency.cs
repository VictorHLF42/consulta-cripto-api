using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Domain
{
    public class CryptoCurrency
    {
        public int Id { get; set; }
        public string? Symbol { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}