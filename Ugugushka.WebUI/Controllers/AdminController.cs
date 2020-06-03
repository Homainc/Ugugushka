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
using Ugugushka.WebUI.ViewModels;

namespace Ugugushka.WebUI.Controllers
{
    [Authorize(Roles = RoleDefaults.Admin)]
    public class AdminController : AbstractController
    {
        private const int ToysPageSize = 10;

        private readonly IToyManager _toyManager;
        private readonly IPictureManager _pictureManager;

        public AdminController(IToyManager toyManager, IPictureManager pictureManager, IMapper mapper) : base(mapper)
        {
            _toyManager = toyManager;
            _pictureManager = pictureManager;
        }

        public async Task<IActionResult> Toys([FromQuery] ToyFilterInfo toyFilter, [FromRoute] int page = 1)
        {
            return View(
                (await _toyManager.GetPagedFilteredAsync(toyFilter,
                    new PageInfo {PageNumber = page, PageSize = ToysPageSize})).Map<ToyDto, ToyItemViewModel>(Mapper));
        }

        [HttpGet]
        public IActionResult AddToy() =>
            View(new AddToyViewModel(_pictureManager.Cloudinary));

        public async Task<IActionResult> EditToy(int id)
        {
            var model = Mapper.Map<AddToyViewModel>(await _toyManager.GetByIdAsync(id));
            
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

                TempData["message"] = $"{toy.Name} has been saved";
                return RedirectToAction("Toys");
            }

            toy.Cloudinary = _pictureManager.Cloudinary;
            return View(toy);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _toyManager.DeleteAsync(id);

            return RedirectToAction("Toys");
        }
    }
}
