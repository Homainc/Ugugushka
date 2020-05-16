using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ugugushka.WebUI.HtmlHelpers
{
    public static class TextHelpers
    {
        public static string IsOnStock(this IHtmlHelper html, bool isOnStock)
            => isOnStock ? "Есть" : "Нет";

        public static string DefaultString(this IHtmlHelper hmtl, string str, string defaultValue)
            => string.IsNullOrWhiteSpace(str) ? defaultValue : str;
    }
}
