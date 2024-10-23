using RifkiTestTechnicalSkill.Data;
using RifkiTestTechnicalSkill.Models.DTOs;
using RifkiTestTechnicalSkill.Models;
using RifkiTestTechnicalSkill.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace RifkiTestTechnicalSkill.Services
{
    public class StockService : IStockService
    {
        private readonly ApplicationDbContext _context;

        public StockService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Stock?> GetStockByProductId(Guid productId) => await _context.Stocks.FirstOrDefaultAsync(s => s.ProductId == productId);

        public async Task ManageStock(StockDTO stockToManage)
        {
            // if there is no stock for given product id, then add new record
            // if there is already stock for given product id, update stock's quantity
            var existingStock = await GetStockByProductId(stockToManage.ProductId);
            if (existingStock is null)
            {
                var stock = new Stock { ProductId = stockToManage.ProductId, Quantity = stockToManage.Quantity };
                _context.Stocks.Add(stock);
            }
            else
            {
                existingStock.Quantity = stockToManage.Quantity;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<StockDisplayModel>> GetStocks(string sTerm = "")
        {
            var stocks = await (from product in _context.Products
                                join stock in _context.Stocks
                                on product.Id equals stock.ProductId
                                into book_stock
                                from bookStock in book_stock.DefaultIfEmpty()
                                where string.IsNullOrWhiteSpace(sTerm) || product.ProductName.ToLower().Contains(sTerm.ToLower())
                                select new StockDisplayModel
                                {
                                    ProductId = product.Id,
                                    ProductName = product.ProductName,
                                    Quantity = bookStock == null ? 0 : bookStock.Quantity
                                }
                                ).ToListAsync();
            return stocks;
        }
    }
}
