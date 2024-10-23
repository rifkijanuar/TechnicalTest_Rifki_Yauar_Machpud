using RifkiTestTechnicalSkill.Models.DTOs;
using RifkiTestTechnicalSkill.Models;

namespace RifkiTestTechnicalSkill.Services.Interface
{
    public interface IUserOrderService
    {
        Task<IEnumerable<Order>> UserOrders(bool getAll = false);
        Task ChangeOrderStatus(UpdateOrderStatusModel data);
        Task TogglePaymentStatus(Guid orderId);
        Task<Order?> GetOrderById(Guid id);
        Task<IEnumerable<OrderStatus>> GetOrderStatuses();
    }
}
