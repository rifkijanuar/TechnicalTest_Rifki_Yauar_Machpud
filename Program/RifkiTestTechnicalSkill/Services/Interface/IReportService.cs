using RifkiTestTechnicalSkill.Models.DTOs;

namespace RifkiTestTechnicalSkill.Services.Interface
{
    public interface IReportService
    {
        Task<IEnumerable<TopNSoldProductModel>> GetTopNSellingProductsByDate(DateTime startDate, DateTime endDate);
    }
}
