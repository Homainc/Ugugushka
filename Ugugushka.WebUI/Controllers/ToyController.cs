using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Ugugushka.Common.Concretes;
using Ugugushka.Domain.Code.Extensions;
using Ugugushka.Domain.Code.Interfaces;
using Ugugushka.Domain.DtoModels;
using Ugugushka.WebUI.Code.Abstractions;
using Ugugushka.WebUI.ViewModels;

namespace Ugugushka.WebUI.Controllers
{
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
        public IActionResult AddToy()
        {
            return View(new AddToyViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddToy(AddToyViewModel toy)
        {
            if (ModelState.IsValid)
            {
                await _toyManager.CreateAsync(Mapper.Map<AddToyViewModel, ToyCreateDto>(toy));
                TempData["message"] = string.Format("{0} has been saved", toy.Name);
                return RedirectToAction("Toys");
            }
            else
            {
                return View(toy);
            }
        }

        public async Task<IActionResult> ToyInfo([FromRoute] int id)
        {
            return View(Mapper.Map<ToyDto, ToyItemViewModel>(await _toyManager.GetByIdAsync(id)));
        }
    }
}
