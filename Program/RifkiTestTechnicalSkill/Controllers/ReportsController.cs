using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RifkiTestTechnicalSkill.Constants;
using RifkiTestTechnicalSkill.Models.DTOs;
using RifkiTestTechnicalSkill.Services.Interface;

namespace RifkiTestTechnicalSkill.Controllers
{
    [Authorize(Roles = nameof(Roles.Admin))]
    public class ReportsController : Controller
    {
        private readonly IReportService _reportService;
        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }
        // GET: ReportsController
        public async Task<ActionResult> TopFiveSellingProducts(DateTime? sDate = null, DateTime? eDate = null)
        {
            try
            {
                // by default, get last 7 days record
                DateTime startDate = sDate ?? DateTime.UtcNow.AddDays(-7);
                DateTime endDate = eDate ?? DateTime.UtcNow;
                var topFiveSellingBooks = await _reportService.GetTopNSellingProductsByDate(startDate, endDate);
                var vm = new TopNSoldProductsVm(startDate, endDate, topFiveSellingBooks);
                return View(vm);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Something went wrong";
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
