using RifkiTestTechnicalSkill.Models;

namespace RifkiTestTechnicalSkill.Services.Interface
{
    public interface IProductService
    {
        Task AddProduct(Product product);
        Task DeleteProduct(Product product);
        Task<Product?> GetProductById(Guid id);
        Task<IEnumerable<Product>> GetProducts();
        Task UpdateProduct(Product product);
    }
}
