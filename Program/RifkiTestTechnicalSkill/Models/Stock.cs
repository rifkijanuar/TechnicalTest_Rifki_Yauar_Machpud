using System.ComponentModel.DataAnnotations.Schema;

namespace RifkiTestTechnicalSkill.Models
{
    [Table("Stocks")]
    public class Stock
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        public Product? Products { get; set; }
    }
}
