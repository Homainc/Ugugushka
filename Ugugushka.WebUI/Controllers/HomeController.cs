using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ugugushka.Common.Concretes;
using Ugugushka.Domain.Code.Interfaces;
using Ugugushka.WebUI.Code.Abstractions;
using Ugugushka.WebUI.ViewModels;

namespace Ugugushka.WebUI.Controllers
{
    public class HomeController : AbstractController
    {
        private const int ToysPageSize = 20;
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

        public async Task<IActionResult> ToyInfo([FromRoute] int id)
        {
            var model = new HomeToyInfoViewModel(
                await _toyManager.GetByIdAsync(id),
                _pictureManager.Cloudinary);

            if (model.Toy.CategoryId.HasValue)
                model.SimilarToys = await _toyManager.GetSimilarToysAsync(model.Toy.CategoryId.Value, model.Toy.Id);

            return View(model);
        }

        public IActionResult Contacts() => View();

        public IActionResult Error([FromRoute] int code) => View(code);

        public async Task<IActionResult> SiteMap([FromServices] ISiteMapManager siteMapManager)
        {
            var xml = await siteMapManager.GetSiteMapDocumentAsync();

            return Content(xml, "text/xml", Encoding.UTF8);
        }
    }
}
