using Microsoft.EntityFrameworkCore;
using RifkiTestTechnicalSkill.Data;
using RifkiTestTechnicalSkill.Models;
using RifkiTestTechnicalSkill.Services.Interface;

namespace RifkiTestTechnicalSkill.Services
{
    public class HomeService : IHomeService
    {
        private readonly ApplicationDbContext _db;

        public HomeService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Genre>> Genres()
        {
            return await _db.Genres.ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetProducts(string sTerm = "", int genreId = 0)
        {
            sTerm = sTerm == null ? "": sTerm.ToLower();
            IEnumerable<Product> products = await (from book in _db.Products
                                             join genre in _db.Genres
                                             on book.GenreId equals genre.Id
                                             join stock in _db.Stocks
                                             on book.Id equals stock.ProductId
                                             into book_stocks
                                             from bookWithStock in book_stocks.DefaultIfEmpty()
                                             where string.IsNullOrWhiteSpace(sTerm) || (book != null && book.ProductName.ToLower().StartsWith(sTerm))
                                             select new Product
                                             {
                                                 Id = book.Id,
                                                 Image = book.Image,
                                                 AuthorName = book.AuthorName,
                                                 ProductName = book.ProductName,
                                                 GenreId = book.GenreId,
                                                 Price = book.Price,
                                                 GenreName = genre.GenreName,
                                                 Quantity = bookWithStock == null ? 0 : bookWithStock.Quantity
                                             }
                         ).ToListAsync();
            if (genreId > 0)
            {

                products = products.Where(a => a.GenreId == genreId).ToList();
            }
            return products;

        }
    }
}
