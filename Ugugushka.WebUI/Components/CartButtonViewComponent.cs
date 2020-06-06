using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ugugushka.WebUI.Code.Extensions;

namespace Ugugushka.WebUI.Components
{
    public class CartButtonViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke() 
            => View(HttpContext.Session?.GetCartFromSession());
    }
}
