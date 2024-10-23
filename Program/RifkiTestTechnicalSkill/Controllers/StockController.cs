using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RifkiTestTechnicalSkill.Constants;
using RifkiTestTechnicalSkill.Models;
using RifkiTestTechnicalSkill.Models.DTOs;
using RifkiTestTechnicalSkill.Services.Interface;

namespace RifkiTestTechnicalSkill.Controllers
{
    [Authorize(Roles = nameof(Roles.Admin))]
    public class StockController : Controller
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        public async Task<IActionResult> Index(string sTerm = "")
        {
            var stocks = await _stockService.GetStocks(sTerm);
            return View(stocks);
        }

        public async Task<IActionResult> ManangeStock(Guid bookId)
        {
            var existingStock = await _stockService.GetStockByProductId(bookId);
            var stock = new StockDTO
            {
                ProductId = bookId,
                Quantity = existingStock != null
            ? existingStock.Quantity : 0
            };
            return View(stock);
        }

        [HttpPost]
        public async Task<IActionResult> ManangeStock(StockDTO stock)
        {
            if (!ModelState.IsValid)
                return View(stock);
            try
            {
                await _stockService.ManageStock(stock);
                TempData["successMessage"] = "Stock is updated successfully.";
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Something went wrong!!";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
