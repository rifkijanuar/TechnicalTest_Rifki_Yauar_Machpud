using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RifkiTestTechnicalSkill.Services.Interface;

namespace RifkiTestTechnicalSkill.Controllers
{
    [Authorize]
    public class UserOrderController : Controller
    {
        private readonly IUserOrderService _userOrderService;

        public UserOrderController(IUserOrderService userOrderService)
        {
            _userOrderService = userOrderService;
        }
        public async Task<IActionResult> UserOrders()
        {
            var orders = await _userOrderService.UserOrders();
            return View(orders);
        }
    }
}
