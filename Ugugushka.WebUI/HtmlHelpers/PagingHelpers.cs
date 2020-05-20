using System;
using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ugugushka.WebUI.HtmlHelpers
{
    public static class PagingHelpers
    {
        private static TagBuilder CreatePageLinkElement(string text, string link, bool selected = false)
        {
            var tag = new TagBuilder("a");

            tag.MergeAttribute("href", link);
            tag.AddCssClass("d-flex btn btn-primary");
            tag.InnerHtml.Append(text);

            if (selected)
                tag.AddCssClass("selected");

            return tag;
        }

        public static HtmlString PageLinks(this IHtmlHelper html, int pageNumber, int totalPages,
            Func<int, string> pageUrl)
        {
            var result = new StringWriter();

            for (int i = 1; i <= totalPages; i++)
                CreatePageLinkElement(i.ToString(), pageUrl(i), i == pageNumber)
                    .WriteTo(result, HtmlEncoder.Default);

            return new HtmlString(result.ToString());
        }
    }
}
