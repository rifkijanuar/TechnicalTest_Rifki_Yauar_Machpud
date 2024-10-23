using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace RifkiTestTechnicalSkill.Models
{
    [Table("ShoppingCarts")]
    public class ShoppingCart
    {
        public Guid Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public bool IsDeleted { get; set; } = false;

        public ICollection<CartDetail> CartDetails { get; set; }

    }
}
