using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ugugushka.Common.Concretes;
using Ugugushka.Data.Models;
using Ugugushka.Domain.Code.Constants;
using Ugugushka.Domain.Code.Interfaces;
using Ugugushka.Domain.DtoModels;
using Ugugushka.WebUI.Code.Abstractions;
using Ugugushka.WebUI.Code.Constants;
using Ugugushka.WebUI.Code.Extensions;
using Ugugushka.WebUI.ViewModels;

namespace Ugugushka.WebUI.Controllers
{
    [Authorize(Roles = RoleDefaults.Admin)]
    public class AdminController : AbstractController
    {
        private const int ToysPageSize = 20;
        private const int OrdersPageSize = 20;
        private const int PartitionsPageSize = 20;
        private const int CategoriesPageSize = 20;

        private readonly IToyManager _toyManager;
        private readonly IPictureManager _pictureManager;
        private readonly IOrderManager _orderManager;
        private readonly IPartitionManager _partitionManager;
        private readonly ICategoryManager _categoryManager;

        public AdminController(
            IPartitionManager partitionManager, ICategoryManager categoryManager,
            IToyManager toyManager, IPictureManager pictureManager, 
            IOrderManager orderManager, IMapper mapper) : base(mapper)
        {
            _toyManager = toyManager;
            _pictureManager = pictureManager;
            _orderManager = orderManager;
            _partitionManager = partitionManager;
            _categoryManager = categoryManager;
        }

        public async Task<IActionResult> Toys([FromQuery] ToyFilterInfo toyFilter, [FromQuery] int page = 1) => 
            View(new AdminToysViewModel {
                PagedToys = await _toyManager.GetPagedFilteredAsync(toyFilter, new PageInfo {PageNumber = page, PageSize = ToysPageSize})
            });

        [HttpGet]
        public async Task<IActionResult> AddToy()
        {
            ViewBag.Title = PageNameDefaults.AddToy.WithDomain();
            ViewBag.ActionTitle = PageNameDefaults.AddToy;
            ViewBag.CategoriesList = new SelectList(await _categoryManager.GetAllAsync(), "Id", "Name", null, "Partition.Name");

            return View(new AddToyViewModel(_pictureManager.Cloudinary));
        }

        public async Task<IActionResult> EditToy(int id)
        {

            var toy = await _toyManager.GetByIdAsync(id);
            var model = Mapper.Map<AddToyViewModel>(toy);

            ViewBag.Title = PageNameDefaults.EditToy.WithDomain();
            ViewBag.ActionTitle = PageNameDefaults.EditToy;
            ViewBag.CategoriesList = new SelectList(await _categoryManager.GetAllAsync(), "Id", "Name", toy.CategoryId, "Partition.Name");

            model.Cloudinary = _pictureManager.Cloudinary;
            return View(nameof(AddToy), model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToy(AddToyViewModel model)
        {
            if (ModelState.IsValid)
            {
                var createdToy = await _toyManager.SaveAsync(Mapper.Map<AddToyViewModel, ToyUpdateDto>(model));

                TempData["message"] = $"{createdToy.Name} был(а) успешно сохранён(а)!";
                
                return RedirectToAction(nameof(Toys));
            }

            var actionName = model.Id == 0 ? PageNameDefaults.AddToy : PageNameDefaults.EditToy;
            ViewBag.Title = actionName.WithDomain();
            ViewBag.ActionTitle = actionName;
            ViewBag.CategoriesList = new SelectList(await _categoryManager.GetAllAsync(), "Id", "Name", 
                model.CategoryId == 0? null : model.CategoryId, "Partition.Name");
            model.Cloudinary = _pictureManager.Cloudinary;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedToy = await _toyManager.DeleteAsync(id);

            TempData["message"] = $"{deletedToy.Name} был(а) успешно удален(а)!";
            
            return RedirectToAction(nameof(Toys));
        }

        public async Task<IActionResult> Orders([FromQuery] int page = 1)
        {
            return View(new AdminOrdersViewModel
            {
                PagedOrders = await _orderManager.GetFilteredPagedAsync(new OrderFilterInfo(),
                    new PageInfo {PageNumber = page, PageSize = OrdersPageSize})
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

            return RedirectToAction(nameof(OrderInfo), new {Id = model.OrderId, ReturnUrl = returnUrl});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            await _orderManager.DeleteAsync(id);

            TempData["message"] = "Заказ успешно удалён!";

            return RedirectToAction(nameof(Orders));
        }

        public async Task<IActionResult> Partitions([FromQuery] int page = 1) =>
            View(new AdminPartitionsViewModel { 
                PagedPartitions = await _partitionManager.GetPagedAsync(new PageInfo { 
                    PageNumber = page, 
                    PageSize = PartitionsPageSize
                })
            });

        public IActionResult AddPartition()
        {
            ViewBag.Title = PageNameDefaults.AddPartition.WithDomain();
            ViewBag.ActionTitle = PageNameDefaults.AddPartition;

            return View(new AdminAddPartitionViewModel());
        }

        public async Task<IActionResult> UpdatePartition([FromRoute] int id)
        {
            ViewBag.Title = PageNameDefaults.EditPartition.WithDomain();
            ViewBag.ActionTitle = PageNameDefaults.EditPartition;

            return View(nameof(AddPartition), Mapper.Map<AdminAddPartitionViewModel>(await _partitionManager.GetByIdAsync(id)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPartition(AdminAddPartitionViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _partitionManager.SaveAsync(Mapper.Map<PartitionDto>(model));

                TempData["message"] = $"Раздел \"{model.Name}\" успешно сохранён!";

                return RedirectToAction(nameof(Partitions));
            }

            var actionName = model.Id == 0 ? PageNameDefaults.AddPartition : PageNameDefaults.EditPartition;
            ViewBag.Title = actionName.WithDomain();
            ViewBag.ActionTitle = actionName;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePartition(int id)
        {
            var partition = await _partitionManager.DeleteAsync(id);

            TempData["message"] = $"Раздел \"{partition.Name}\" успешно удалён!";

            return RedirectToAction(nameof(Partitions));
        }

        public async Task<IActionResult> Categories([FromQuery] int page = 1) =>
            View(new AdminCategoriesViewModel { 
                PagedCategories = await _categoryManager.GetAllPagedAsync(new PageInfo { 
                    PageNumber = page,
                    PageSize = CategoriesPageSize
                })
            });

        public async Task<IActionResult> AddCategory()
        {
            ViewBag.Title = PageNameDefaults.AddCategory.WithDomain();
            ViewBag.ActionTitle = PageNameDefaults.AddCategory;
            ViewBag.PartitionsList = new SelectList(await _partitionManager.GetAllAsync(), "Id", "Name", null);

            return View(new AdminAddCategoryViewModel());
        }

        public async Task<IActionResult> UpdateCategory([FromRoute] int id)
        {
            var model = Mapper.Map<AdminAddCategoryViewModel>(await _categoryManager.GetByIdAsync(id));

            ViewBag.Title = PageNameDefaults.EditCategory.WithDomain();
            ViewBag.ActionTitle = PageNameDefaults.EditCategory;
            ViewBag.PartitionsList = new SelectList(await _partitionManager.GetAllAsync(), "Id", "Name", model.PartitionId);

            return View(nameof(AddCategory), model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCategory(AdminAddCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _categoryManager.SaveAsync(Mapper.Map<CategoryDto>(model));

                TempData["message"] = $"Категория \"{model.Name}\" успешно сохранена!";

                return RedirectToAction(nameof(Categories));
            }

            var actionName = model.Id == 0 ? PageNameDefaults.AddCategory : PageNameDefaults.EditCategory;
            ViewBag.Title = actionName.WithDomain();
            ViewBag.ActionTitle = actionName;
            ViewBag.PartitionsList = new SelectList(await _partitionManager.GetAllAsync(), "Id", "Name", model.PartitionId);
            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _categoryManager.DeleteAsync(id);

            TempData["message"] = $"Категория \"{category.Name}\" успешно удалена!";

            return RedirectToAction(nameof(Categories));
        }
    }
}
