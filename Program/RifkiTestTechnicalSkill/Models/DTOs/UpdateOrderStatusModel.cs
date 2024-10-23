using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace RifkiTestTechnicalSkill.Models.DTOs
{
    public class UpdateOrderStatusModel
    {
        public Guid OrderId { get; set; }

        [Required]
        public Guid OrderStatusId { get; set; }

        public IEnumerable<SelectListItem>? OrderStatusList { get; set; }
    }
}
