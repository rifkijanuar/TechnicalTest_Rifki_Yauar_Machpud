using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace RifkiTestTechnicalSkill.Models
{
    [Table("CartDetails")]
    public class CartDetail
    {
        public Guid Id { get; set; }
        [Required]
        public Guid ShoppingCartId { get; set; }
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double UnitPrice { get; set; }
        public Product Products { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
    }
}
