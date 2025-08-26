using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class CryptoCurrency
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "TEXT")]
        public string? Symbol { get; set; }
        [Column(TypeName = "REAL")]
        public decimal Price { get; set; }
        [Column(TypeName = "DATETIME")]
        public DateTime CreatedAt{ get; set; }


    }
}
