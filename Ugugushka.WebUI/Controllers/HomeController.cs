using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ugugushka.Common.Concretes;
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

        public async Task<IActionResult> Index([FromQuery] ToyFilterInfo filter, Cart cart, int page = 1)
        {
            return View(new HomeIndexViewModel(_pictureManager.Cloudinary) {
                    PagedToys = await _toyManager.GetPagedFilteredAsync(filter, new PageInfo {PageNumber = page, PageSize = ToysPageSize})});
        }

        public async Task<IActionResult> ToyInfo([FromRoute] int id)
        {
            var model = new ToyInfoViewModel(_pictureManager.Cloudinary);
            Mapper.Map(await _toyManager.GetByIdAsync(id), model);
            
            return View(model);
        }

        public IActionResult Error() => View();
    }
}
