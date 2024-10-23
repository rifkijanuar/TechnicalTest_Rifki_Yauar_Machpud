using Microsoft.EntityFrameworkCore;
using RifkiTestTechnicalSkill.Data;
using RifkiTestTechnicalSkill.Models;
using RifkiTestTechnicalSkill.Services.Interface;

namespace RifkiTestTechnicalSkill.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<Product?> GetProductById(Guid id) => await _context.Products.FindAsync(id);

        public async Task<IEnumerable<Product>> GetProducts() => await _context.Products.Include(a => a.Genre).ToListAsync();
    }
}
