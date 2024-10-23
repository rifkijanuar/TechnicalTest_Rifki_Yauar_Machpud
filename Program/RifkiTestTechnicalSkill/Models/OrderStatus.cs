using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RifkiTestTechnicalSkill.Models
{
    [Table("OrderStatuses")]
    public class OrderStatus
    {
        public Guid Id { get; set; }
        [Required]
        public int StatusId { get; set; }
        [Required, MaxLength(20)]
        public string? StatusName { get; set; }
    }
}
