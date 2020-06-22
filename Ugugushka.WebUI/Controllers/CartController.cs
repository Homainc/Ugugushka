using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Ugugushka.Domain.Code.Config;
using Ugugushka.Domain.Code.Interfaces;
using Ugugushka.Domain.DtoModels;
using Ugugushka.WebUI.Code.Abstractions;
using Ugugushka.WebUI.Code.Constants;
using Ugugushka.WebUI.Code.Extensions;
using Ugugushka.WebUI.ViewModels;

namespace Ugugushka.WebUI.Controllers
{
    [Authorize]
    public class CartController : AbstractController
    {
        private readonly DeliveryConfig _deliveryConfig;

        private readonly IToyManager _toyManager;
        private readonly IOrderManager _orderManager;
        private readonly Cloudinary _cloudinary;
        public CartController(IPictureManager pictureManager, IToyManager toyManager, IOptions<DeliveryConfig> deliveryOptions, IOrderManager orderManager, IMapper mapper) : base(mapper)
        {
            _cloudinary = pictureManager.Cloudinary;
            _orderManager = orderManager;
            _toyManager = toyManager;
            _deliveryConfig = deliveryOptions.Value;
        }

        public IActionResult Index(Cart cart, string returnUrl) =>
            View(new CartIndexViewModel(_cloudinary)
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });

        public async Task<RedirectToActionResult> AddToCart(Cart cart, int id, int quantity, string returnUrl)
        {
            var toy = await _toyManager.GetByIdAsync(id);
            if (toy != null)
            {
                cart.AddItem(toy, quantity);
                HttpContext.Session.SetComplexData("Cart", cart);
            }

            return RedirectToAction("Index", new {returnUrl});
        }

        public async Task<RedirectToActionResult> RemoveFromCart(Cart cart, int id, string returnUrl)
        {
            var toy = await _toyManager.GetByIdAsync(id);
            if (toy != null)
            {
                cart.RemoveLine(toy);
                HttpContext.Session.SetComplexData(SessionKeyDefaults.Cart, cart);
            }

            return RedirectToAction("Index", new {returnUrl});
        }

        public RedirectToActionResult Clear(Cart cart, string returnUrl)
        {
            cart.Clear();
            HttpContext.Session.SetComplexData(SessionKeyDefaults.Cart, cart);

            return RedirectToAction("Index", new {returnUrl});
        }

        public IActionResult Checkout(Cart cart)
        {
            if (!cart.Lines.Any())
                return RedirectToAction("Index");

            ViewBag.CourierPrice = _deliveryConfig.CourierPrice;
            return View(new CartCheckoutViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(Cart cart, CartCheckoutViewModel model)
        {
            if (ModelState.IsValid)
            {
                var order = await _orderManager.CreateAsync(Mapper.Map<OrderDtoCreate>(model), cart);

                TempData["message"] = "Заказ успешно добавлен. Ожидайте звонка оператора.";
                return View("OrderDetails", order);
            }
            else
            {
                return View(model);
            }
        }
    }
}
