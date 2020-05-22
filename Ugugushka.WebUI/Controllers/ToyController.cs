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
    [Authorize]
    public class ToyController : AbstractController
    {
        private readonly IToyManager _toyManager;
        private const int ToysPageSize = 10;

        public ToyController(IToyManager toyManager, IMapper mapper) : base(mapper) =>
            _toyManager = toyManager;

        public async Task<IActionResult> Toys([FromQuery] ToyFilterInfo toyFilter, [FromRoute]int page = 1)
        {
            return View(
                (await _toyManager.GetPagedFilteredAsync(toyFilter,
                    new PageInfo {PageNumber = page, PageSize = ToysPageSize})).Map<ToyDto, ToyItemViewModel>(Mapper));
        }

        [HttpGet]
        [Authorize(Roles = RoleDefaults.Admin)]
        public IActionResult AddToy() => 
            View(new AddToyViewModel());

        [HttpPost]
        [Authorize(Roles = RoleDefaults.Admin)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToy(AddToyViewModel toy)
        {
            if (ModelState.IsValid)
            {
                await _toyManager.CreateAsync(Mapper.Map<AddToyViewModel, ToyCreateDto>(toy));
                TempData["message"] = $"{toy.Name} has been saved";
                return RedirectToAction("Toys");
            }
            else
            {
                return View(toy);
            }
        }

        public async Task<IActionResult> ToyInfo([FromRoute] int id) =>
            View(new ToyInfoViewModel {Toy = await _toyManager.GetByIdAsync(id)});
    }
}
