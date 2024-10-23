using RifkiTestTechnicalSkill.Models.DTOs;
using RifkiTestTechnicalSkill.Models;

namespace RifkiTestTechnicalSkill.Services.Interface
{
    public interface ICartService
    {
        Task<int> AddItem(Guid productId, int qty);
        Task<int> RemoveItem(Guid productId); 
        Task<ShoppingCart> GetUserCart();
        Task<int> GetCartItemCount(string userId = "");
        Task<ShoppingCart> GetCart(string userId);
        Task<bool> DoCheckout(CheckoutModel model);
    }
}
