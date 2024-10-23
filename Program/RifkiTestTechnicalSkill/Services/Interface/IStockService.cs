using RifkiTestTechnicalSkill.Models.DTOs;
using RifkiTestTechnicalSkill.Models;

namespace RifkiTestTechnicalSkill.Services.Interface
{
    public interface IStockService
    {
        Task<IEnumerable<StockDisplayModel>> GetStocks(string sTerm = "");
        Task<Stock?> GetStockByProductId(Guid bookId);
        Task ManageStock(StockDTO stockToManage);
    }
}
