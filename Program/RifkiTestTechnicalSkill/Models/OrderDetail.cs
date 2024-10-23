using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace RifkiTestTechnicalSkill.Models
{
    [Table("OrderDetail")]
    public class OrderDetail
    {
        public Guid Id { get; set; }
        [Required]
        public Guid OrderId { get; set; }
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double UnitPrice { get; set; }
        public Order Order { get; set; }
        public Product Products { get; set; }
    }
}
