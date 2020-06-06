using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Ugugushka.Domain.DtoModels;
using Ugugushka.WebUI.Code.Constants;

namespace Ugugushka.WebUI.Code.Extensions
{
    internal static class SessionExtensions
    {
        public static T GetComplexData<T>(this ISession session, string key)
        {
            var data = session.GetString(key);

            return data != null ? JsonConvert.DeserializeObject<T>(data) : default;
        }

        public static void SetComplexData(this ISession session, string key, object value) =>
            session.SetString(key, JsonConvert.SerializeObject(value));

        public static Cart GetCartFromSession(this ISession session)
        {
            var cart = session?.GetComplexData<Cart>(SessionKeyDefaults.Cart);

            if (cart != null)
                return cart;

            cart = new Cart();
            session?.SetComplexData(SessionKeyDefaults.Cart, cart);

            return cart;
        }
    }
}
