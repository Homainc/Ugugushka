using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ugugushka.WebUI.HtmlHelpers
{
    public static class PagingHelpers
    {
        private const string HtmlLeftArrow = "<i class='fas fa-chevron-left'></i>";
        private const string HtmlRightArrow = "<i class='fas fa-chevron-right'></i>";

        private static TagBuilder CreatePageLinkTag(string text, string url, bool selected = false, bool disabled = false)
        {
            var tag = new TagBuilder("a");
            
            tag.MergeAttribute("href", url ?? "#");
            tag.AddCssClass("page-link");
            tag.InnerHtml.AppendHtml(text);
            if (selected) tag.AddCssClass("selected");
            if (disabled) tag.AddCssClass("d-none");
            
            return tag;
        }

        public static TagBuilder CreatePageLinks(this IHtmlHelper html, int pageNumber, int totalPages, Func<int, string> pageUrl, bool outlined = false)
        {
            if (totalPages <= 1)
                return null;

            var div = new TagBuilder("div");
            div.AddCssClass($"d-flex page-links{(outlined? "-outlined":"")}");

            div.InnerHtml
                .AppendHtml(CreatePageLinkTag(HtmlLeftArrow, pageUrl(pageNumber - 1), disabled: pageNumber - 1 < 1));

            if(pageNumber != 1)
                div.InnerHtml
                    .AppendHtml(CreatePageLinkTag("1", pageUrl(1), selected: pageNumber == 1));

            if (pageNumber > 2)
            {
                if(pageNumber - 1 > 2)
                    div.InnerHtml
                        .AppendHtml(CreatePageLinkTag("..", null));
                div.InnerHtml
                    .AppendHtml(CreatePageLinkTag((pageNumber - 1).ToString(), pageUrl(pageNumber - 1)));
            }

            div.InnerHtml.AppendHtml(CreatePageLinkTag(pageNumber.ToString(), pageUrl(pageNumber), selected: true));

            if (pageNumber < totalPages - 1)
            {
                div.InnerHtml
                    .AppendHtml(CreatePageLinkTag((pageNumber + 1).ToString(), pageUrl(pageNumber + 1)));

                if (totalPages - pageNumber > 2)
                    div.InnerHtml
                        .AppendHtml(CreatePageLinkTag("..", null));
            }

            if (pageNumber != totalPages)
                div.InnerHtml
                    .AppendHtml(CreatePageLinkTag(totalPages.ToString(), pageUrl(totalPages), selected: pageNumber == totalPages));

            div.InnerHtml.AppendHtml(
                CreatePageLinkTag(HtmlRightArrow, pageUrl(pageNumber + 1), disabled: pageNumber + 1 > totalPages));

            return div;
        }
    }
}
