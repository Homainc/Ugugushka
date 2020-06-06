using System;
using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ugugushka.WebUI.HtmlHelpers
{
    public static class PagingHelpers
    {
        private static TagBuilder CreatePageLinkElement(string text, string link, string className, bool selected = false)
        {
            var tag = new TagBuilder("a");

            tag.MergeAttribute("href", link);
            tag.AddCssClass($"d-flex btn {className}");
            tag.InnerHtml.Append(text);

            if (selected)
                tag.AddCssClass($"{className}-hover");

            return tag;
        }

        public static HtmlString PageLinks(this IHtmlHelper html, int pageNumber, int totalPages, string className,
            Func<int, string> pageUrl)
        {
            var result = new StringWriter();

            for (int i = 1; i <= totalPages; i++)
                CreatePageLinkElement(i.ToString(), pageUrl(i), className, i == pageNumber)
                    .WriteTo(result, HtmlEncoder.Default);

            return new HtmlString(result.ToString());
        }
    }
}
