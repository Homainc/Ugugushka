using Microsoft.AspNetCore.Mvc;
using Ugugushka.WebUI.Code.Extensions;

namespace Ugugushka.WebUI.Components
{
    public class CartOverviewViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
            => View(HttpContext.Session?.GetCartFromSession());
    }
}
