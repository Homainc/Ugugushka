using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ugugushka.Domain.Code.Interfaces;
using Ugugushka.Domain.DtoModels;
using Ugugushka.WebUI.Code.Abstractions;
using Ugugushka.WebUI.Code.Extensions;
using Ugugushka.WebUI.ViewModels;

namespace Ugugushka.WebUI.Controllers
{
    [Authorize]
    public class CartController : AbstractController
    {
        private readonly IToyManager _toyManager;
        private readonly Cloudinary _cloudinary;
        public CartController(IPictureManager pictureManager, IToyManager toyManager, IMapper mapper) : base(mapper)
        {
            _cloudinary = pictureManager.Cloudinary;
            _toyManager = toyManager;
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
                HttpContext.Session.SetComplexData("Cart", cart);
            }

            return RedirectToAction("Index", new {returnUrl});
        }

        public PartialViewResult CartButton(Cart cart) => PartialView(cart);
    }
}
