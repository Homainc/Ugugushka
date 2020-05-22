using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ugugushka.Common.Concretes;
using Ugugushka.Domain.Code.Extensions;
using Ugugushka.Domain.Code.Interfaces;
using Ugugushka.Domain.DtoModels;
using Ugugushka.WebUI.Code.Abstractions;
using Ugugushka.WebUI.ViewModels;

namespace Ugugushka.WebUI.Controllers
{
    [Authorize]
    public class HomeController : AbstractController
    {
        private const int ToysPageSize = 10;
        private readonly IToyManager _toyManager;
        public HomeController(IToyManager toyManager, IMapper mapper) : base(mapper)
        {
            _toyManager = toyManager;
        }

        public async Task<IActionResult> Index([FromQuery] ToyFilterInfo filter, int page = 1)
        {
            return View((await _toyManager.GetPagedFilteredAsync(filter,
                new PageInfo {PageNumber = page, PageSize = ToysPageSize})).Map<ToyDto, ToyItemViewModel>(Mapper));
        }
    }
}
