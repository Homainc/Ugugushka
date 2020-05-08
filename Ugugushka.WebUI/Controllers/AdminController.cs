using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ugugushka.Common.Concretes;
using Ugugushka.Domain.Code.Extensions;
using Ugugushka.Domain.Code.Interfaces;
using Ugugushka.Domain.DtoModels;
using Ugugushka.WebUI.Code.Abstractions;
using Ugugushka.WebUI.ViewModels;

namespace Ugugushka.WebUI.Controllers
{
    public class AdminController : AbstractController
    {
        private readonly IToyManager _toyManager;
        private const int ToysPageSize = 10;

        public AdminController(IToyManager toyManager, IMapper mapper) : base(mapper) =>
            _toyManager = toyManager;

        public async Task<IActionResult> Toys([FromQuery] ToyFilterInfo toyFilter, [FromRoute]int page = 1)
        {
            return View(
                (await _toyManager.GetPagedFilteredAsync(toyFilter,
                    new PageInfo {PageNumber = page, PageSize = ToysPageSize})).Map<ToyDto, ToyItemViewModel>(Mapper));
        }
    }
}
