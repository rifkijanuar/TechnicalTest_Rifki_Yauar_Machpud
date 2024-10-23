using RifkiTestTechnicalSkill.Models;

namespace RifkiTestTechnicalSkill.Services.Interface
{
    public interface IHomeService
    {
        Task<IEnumerable<Product>> GetProducts(string sTerm = "", int genreId = 0);
        Task<IEnumerable<Genre>> Genres();
    }
}
