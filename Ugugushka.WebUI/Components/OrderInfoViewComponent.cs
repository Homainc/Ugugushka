using Microsoft.AspNetCore.Mvc;
using Ugugushka.Domain.DtoModels;

namespace Ugugushka.WebUI.Components
{
    public class OrderInfoViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(OrderDto order, bool withStatus)
        {
            ViewBag.IsOrderStatusVisible = withStatus;
            return View(order);
        }
    }
}
