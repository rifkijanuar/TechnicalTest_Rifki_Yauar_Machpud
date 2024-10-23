namespace RifkiTestTechnicalSkill.Models.DTOs
{
    public class StockDisplayModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public string? ProductName { get; set; }
    }
}
