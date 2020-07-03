using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Ugugushka.Domain.Code.Interfaces;

namespace Ugugushka.WebUI.Components
{
    public class CatalogButtonViewComponent : ViewComponent
    {
        private readonly IPartitionManager _partitionManager;
        public CatalogButtonViewComponent(IPartitionManager partitionManager) =>
            _partitionManager = partitionManager;
        
        public async Task<IViewComponentResult> InvokeAsync() =>
            View(await _partitionManager.GetAllAsync());
    }
}
