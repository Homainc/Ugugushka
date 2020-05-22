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
        private readonly IPictureManager _pictureManager;
        public HomeController(IToyManager toyManager, IPictureManager pictureManager, IMapper mapper) : base(mapper)
        {
            _toyManager = toyManager;
            _pictureManager = pictureManager;
        }

        public async Task<IActionResult> Index([FromQuery] ToyFilterInfo filter, int page = 1)
        {
            return View(new HomeIndexViewModel(_pictureManager.Cloudinary) {
                    PagedToys = await _toyManager.GetPagedFilteredAsync(filter, new PageInfo {PageNumber = page, PageSize = ToysPageSize})});
        }
    }
}
