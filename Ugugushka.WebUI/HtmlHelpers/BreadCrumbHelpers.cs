using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using Ugugushka.Data.Models;
using Ugugushka.Domain.DtoModels;

namespace Ugugushka.WebUI.HtmlHelpers
{
    public static class BreadCrumbHelpers
    {
        private static TagBuilder CreateLink(string text, string url)
        {
            var a = new TagBuilder("a");
            a.InnerHtml.Append(text);
            a.MergeAttribute("href", url);
            return a;
        }

        private static TagBuilder CreateSpan(string text)
        {
            var span = new TagBuilder("span");
            span.InnerHtml.Append(text);
            return span;
        }

        public static TagBuilder BreadCrumb(this IHtmlHelper html, ToyDto toyDto, Func<string, object, string> actionUrl)
        {
            var section = CreateBreadCrumbSection(actionUrl);

            if (toyDto.CategoryId.HasValue)
            {
                section.InnerHtml
                    .AppendHtml(CreateLink(
                        toyDto.Category.Partition.Name,
                        actionUrl("Index", new Dictionary<string, string> { { "filter.PartitionId", toyDto.Category.PartitionId.ToString() } })));

                section.InnerHtml
                    .AppendHtml(CreateLink(
                        toyDto.Category.Name,
                        actionUrl("Index", new Dictionary<string, string> { { "filter.CategoryId", toyDto.CategoryId.ToString() } })));
            }

            section.InnerHtml.AppendHtml(CreateSpan(toyDto.Name));

            return section;
        }

        public static TagBuilder BreadCrumb(this IHtmlHelper html, string currentPageName, Func<string, object, string> actionUrl)
        {
            var section = CreateBreadCrumbSection(actionUrl);

            section.InnerHtml.AppendHtml(CreateSpan(currentPageName));

            return section;
        }

        public static TagBuilder BreadCrumb(this IHtmlHelper html, string currentPageName, IEnumerable<Tuple<string, string>> links)
        {
            var section = CreateBreadCrumbSection();

            foreach (var breadCrumbItem in links)
            {
                section.InnerHtml.AppendHtml(
                    CreateLink(breadCrumbItem.Item1, breadCrumbItem.Item2));
            }

            section.InnerHtml.AppendHtml(CreateSpan(currentPageName));

            return section;
        }

        private static TagBuilder CreateBreadCrumbSection(Func<string, object, string> actionUrl = null)
        {
            var section = new TagBuilder("section");
            section.AddCssClass("breadcrumb");
            
            if(actionUrl != null)
                section.InnerHtml.AppendHtml(CreateLink("Главная", actionUrl("Index", new { })));

            return section;
        }
    }
}
