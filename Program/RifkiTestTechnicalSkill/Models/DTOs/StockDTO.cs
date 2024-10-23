using System.ComponentModel.DataAnnotations;

namespace RifkiTestTechnicalSkill.Models.DTOs
{
    public class StockDTO
    {
        public Guid ProductId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative value.")]
        public int Quantity { get; set; }
    }
}
