using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RifkiTestTechnicalSkill.Constants;
using RifkiTestTechnicalSkill.Models.DTOs;
using RifkiTestTechnicalSkill.Services.Interface;

namespace RifkiTestTechnicalSkill.Controllers
{
    //[Authorize(Roles = nameof(Roles.Admin))]
    public class AdminOperationsController : Controller
    {
        private readonly IUserOrderService _userOrderService;

        public AdminOperationsController(IUserOrderService userOrderService)
        {
            _userOrderService = userOrderService;
        }
        [Authorize(Roles = nameof(Roles.Admin))]
        public async Task<IActionResult> AllOrders()
        {
            var orders = await _userOrderService.UserOrders(true);
            return View(orders);
        }
        [Authorize(Roles = nameof(Roles.Admin))]
        public async Task<IActionResult> TogglePaymentStatus(Guid orderId)
        {
            try
            {
                await _userOrderService.TogglePaymentStatus(orderId);
            }
            catch (Exception ex)
            {
                // log exception here
            }
            return RedirectToAction(nameof(AllOrders));
        }

        [Authorize(Roles = nameof(Roles.Admin))]
        public async Task<IActionResult> UpdateOrderStatus(Guid orderId)
        {
            var order = await _userOrderService.GetOrderById(orderId);
            if (order == null)
            {
                throw new InvalidOperationException($"Order with id:{orderId} does not found.");
            }
            var orderStatusList = (await _userOrderService.GetOrderStatuses()).Select(orderStatus =>
            {
                return new SelectListItem { Value = orderStatus.Id.ToString(), Text = orderStatus.StatusName, Selected = order.OrderStatusId == orderStatus.Id };
            });
            var data = new UpdateOrderStatusModel
            {
                OrderId = orderId,
                OrderStatusId = order.OrderStatusId,
                OrderStatusList = orderStatusList
            };
            return View(data);
        }

        [HttpPost]
        [Authorize(Roles = nameof(Roles.Admin))]
        public async Task<IActionResult> UpdateOrderStatus(UpdateOrderStatusModel data)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    data.OrderStatusList = (await _userOrderService.GetOrderStatuses()).Select(orderStatus =>
                    {
                        return new SelectListItem { Value = orderStatus.Id.ToString(), Text = orderStatus.StatusName, Selected = orderStatus.Id == data.OrderStatusId };
                    });

                    return View(data);
                }
                await _userOrderService.ChangeOrderStatus(data);
                TempData["msg"] = "Updated successfully";
            }
            catch (Exception ex)
            {
                // catch exception here
                TempData["msg"] = "Something went wrong";
            }
            return RedirectToAction(nameof(UpdateOrderStatus), new { orderId = data.OrderId });
        }

        [Authorize(Roles = nameof(Roles.Admin))]
        public IActionResult Dashboard()
        {
            return View();
        }

        [Authorize(Roles = nameof(Roles.User))]
        public IActionResult DashboardUser()
        {
            return View();
        }

        [Authorize(Roles = nameof(Roles.User))]
        public async Task<IActionResult> AllOrdersUsers()
        {
            var orders = await _userOrderService.UserOrders(true);
            return View(orders);
        }

    }
}
