using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Ugugushka.Domain.DtoModels;
using Ugugushka.WebUI.Code.Extensions;

namespace Ugugushka.WebUI.Code.Binders
{
    public class CartModelBinder : IModelBinder
    {
        private const string SessionKey = "Cart";

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var cart = bindingContext.HttpContext.Session?.GetComplexData<Cart>(SessionKey);
            if (cart == null)
            {
                cart = new Cart();
                bindingContext.HttpContext.Session?.SetComplexData(SessionKey, cart);
            }

            bindingContext.Result = ModelBindingResult.Success(cart);
            return Task.CompletedTask;
        }
    }

    public class CartModelBinderProvider : IModelBinderProvider
    {
        private readonly IModelBinder _binder = new CartModelBinder();

        public IModelBinder GetBinder(ModelBinderProviderContext context) =>
            context.Metadata.ModelType == typeof(Cart) ? _binder : null;
    }
}
