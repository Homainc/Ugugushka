using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Ugugushka.Domain.Code.Interfaces;

namespace Ugugushka.WebUI.Components
{
    public class CatalogButtonViewComponent : ViewComponent
    {
        private readonly ICategoryManager _categoryManager;
        public CatalogButtonViewComponent(ICategoryManager categoryManager) =>
            _categoryManager = categoryManager;
        
        public async Task<IViewComponentResult> InvokeAsync() =>
            View(await _categoryManager.GetAllGroupedByPartitionAsync());
    }
}
