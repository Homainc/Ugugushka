using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Ugugushka.Data.Code.Interfaces;
using Ugugushka.Domain.Code.Interfaces;

namespace Ugugushka.Domain.Managers
{
    public class SiteMapManager : ISiteMapManager
    {
        private readonly IToyRepository _toyRepository;
        private readonly IUrlHelper _urlHelper;
        public SiteMapManager(IToyRepository toyRepository, IUrlHelper urlHelper)
        {
            _toyRepository = toyRepository;
            _urlHelper = urlHelper;
        }

        public async Task<string> GetSiteMapDocumentAsync()
        {
            XNamespace xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            var root = new XElement(xmlns + "urlset");

            foreach (var node in await GetSiteMapNodes())
            {
                var urlElement = new XElement(
                    xmlns+"url",
                    new XElement(xmlns + "loc", Uri.EscapeUriString(node)));
                root.Add(urlElement);
            }

            var document = new XDocument(root);
            return document.ToString();
        }

        private async Task<IReadOnlyCollection<string>> GetSiteMapNodes()
        {
            var nodes = new List<string>();

            // Home controller
            nodes.Add(_urlHelper.Link("default", new {controller = "Home", action = "Index"}));
            nodes.Add(_urlHelper.Link("default", new {controller = "Home", action = "Contacts"}));

            var pagesCount = await _toyRepository.GetPagesCountAsync(20);
            for (var i = 1; i <= pagesCount; i++)
                nodes.Add(_urlHelper.Link("toyPage", new {controller = "Home", action = "Index", page = i}));

            nodes.AddRange(from i in await _toyRepository.GetToyIdsAsync()
                select _urlHelper.Link("default", new {controller = "Home", action = "ToyInfo", id = i}));

            // Cart controller
            nodes.Add(_urlHelper.Link("default", new {controller = "Cart", action = "Index"}));

            return nodes;
        }
    }
}
