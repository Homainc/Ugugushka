using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ugugushka.Common.Concretes;
using Ugugushka.Domain.Code.Constants;
using Ugugushka.Domain.Code.Extensions;
using Ugugushka.Domain.Code.Interfaces;
using Ugugushka.Domain.DtoModels;
using Ugugushka.WebUI.Code.Abstractions;
using Ugugushka.WebUI.Code.Constants;
using Ugugushka.WebUI.ViewModels;

namespace Ugugushka.WebUI.Controllers
{
    [Authorize(Roles = RoleDefaults.Admin)]
    public class AdminController : AbstractController
    {
        private const int ToysPageSize = 10;

        private readonly IToyManager _toyManager;
        private readonly IPictureManager _pictureManager;
        private readonly IOrderManager _orderManager;

        public AdminController(IToyManager toyManager, IPictureManager pictureManager, IOrderManager orderManager,
            IMapper mapper) : base(mapper)
        {
            _toyManager = toyManager;
            _pictureManager = pictureManager;
            _orderManager = orderManager;
        }

        public async Task<IActionResult> Toys([FromQuery] ToyFilterInfo toyFilter, [FromQuery] int page = 1)
        {
            return View(
                (await _toyManager.GetPagedFilteredAsync(toyFilter,
                    new PageInfo {PageNumber = page, PageSize = ToysPageSize})).Map<ToyDto, ToyItemViewModel>(Mapper));
        }

        [HttpGet]
        public IActionResult AddToy()
        {
            ViewBag.Title = PageNameDefaults.AddToy.WithDomain();
            ViewBag.ActionTitle = PageNameDefaults.AddToy;

            return View(new AddToyViewModel(_pictureManager.Cloudinary));
        }

        public async Task<IActionResult> EditToy(int id)
        {

            var model = Mapper.Map<AddToyViewModel>(await _toyManager.GetByIdAsync(id));

            ViewBag.Title = PageNameDefaults.EditToy.WithDomain();
            ViewBag.ActionTitle = PageNameDefaults.EditToy;
            model.Cloudinary = _pictureManager.Cloudinary;

            return View("AddToy", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToy(AddToyViewModel toy)
        {
            if (ModelState.IsValid)
            {
                var createdToy = await _toyManager.SaveAsync(Mapper.Map<AddToyViewModel, ToyUpdateDto>(toy));

                TempData["message"] = $"{createdToy.Name} был(а) успешно сохранён(а)!";
                return RedirectToAction("Toys");
            }

            var actionName = toy.Id == 0 ? PageNameDefaults.AddToy : PageNameDefaults.EditToy;
            ViewBag.Title = actionName.WithDomain();
            ViewBag.ActionTitle = actionName;
            toy.Cloudinary = _pictureManager.Cloudinary;
            return View(toy);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedToy = await _toyManager.DeleteAsync(id);

            TempData["message"] = $"{deletedToy.Name} был(а) успешно удален(а)!";
            return RedirectToAction("Toys");
        }

        public async Task<IActionResult> Orders([FromQuery] int page = 1)
        {
            return View(new AdminOrdersViewModel
            {
                PagedOrders = await _orderManager.GetFilteredPagedAsync(new OrderFilterInfo(),
                    new PageInfo {PageNumber = page, PageSize = 10})
            });
        }

        public async Task<IActionResult> OrderInfo([FromRoute] Guid id, string returnUrl)
            => View(new AdminOrderInfoViewModel
            {
                Order = await _orderManager.GetByIdEagerAsync(id),
                ReturnUrl = returnUrl
            });

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetOrderStatus(AdminSetOrderStatusViewModel model, string returnUrl)
        {
            await _orderManager.SetOrderStatusAsync(model.OrderId, model.Status);
            
            TempData["message"] = "Статус заказа успешно изменён!";

            return RedirectToAction("OrderInfo", new {Id = model.OrderId, ReturnUrl = returnUrl});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            await _orderManager.DeleteAsync(id);

            TempData["message"] = "Заказ успешно удалён!";

            return RedirectToAction("Orders");
        }
    }
}
