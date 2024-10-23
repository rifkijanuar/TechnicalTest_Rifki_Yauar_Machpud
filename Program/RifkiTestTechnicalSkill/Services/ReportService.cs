using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RifkiTestTechnicalSkill.Data;
using RifkiTestTechnicalSkill.Models.DTOs;
using RifkiTestTechnicalSkill.Services.Interface;

namespace RifkiTestTechnicalSkill.Services
{
    public class ReportService : IReportService
    {
        private readonly ApplicationDbContext _context;
        public ReportService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TopNSoldProductModel>> GetTopNSellingProductsByDate(DateTime startDate, DateTime endDate)
        {
            var startDateParam = new SqlParameter("@startDate", startDate);
            var endDateParam = new SqlParameter("@endDate", endDate);
            var topFiveSoldBooks = await _context.Database.SqlQueryRaw<TopNSoldProductModel>("exec Usp_GetTopNSellingBooksByDate @startDate,@endDate", startDateParam, endDateParam).ToListAsync();
            return topFiveSoldBooks;
        }
    }
}
