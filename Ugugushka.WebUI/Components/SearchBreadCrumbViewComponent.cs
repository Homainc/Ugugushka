using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ugugushka.Domain.Code.Interfaces;

namespace Ugugushka.WebUI.Components
{
    public class SearchBreadCrumbViewComponent : ViewComponent
    {
        private const string CategoryFilterKey = "filter.CategoryId";
        private const string PartitionFilterKey = "filter.PartitionId";

        private readonly ICategoryManager _categoryManager;
        private readonly IPartitionManager _partitionManager;
        public SearchBreadCrumbViewComponent(ICategoryManager categoryManager, IPartitionManager partitionManager)
        {
            _categoryManager = categoryManager;
            _partitionManager = partitionManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (Request.Query.ContainsKey(CategoryFilterKey) &&
                int.TryParse(Request.Query[CategoryFilterKey], out int catId))
            {
                var category = await _categoryManager.GetByIdAsync(catId);

                ViewBag.CurrentFilter = category.Name;
                ViewBag.BreadCrumbsLinks = new Tuple<string, string>[] {
                    Tuple.Create("Главная", Url.Action("Index")),
                    Tuple.Create(category.Partition.Name, Url.Action("Index", new Dictionary<string, string>{
                        { PartitionFilterKey, category.PartitionId.ToString() }
                    }))
                };
            }
            else if (Request.Query.ContainsKey(PartitionFilterKey) && 
                int.TryParse(Request.Query[PartitionFilterKey], out int partId))
            {
                var partition = await _partitionManager.GetByIdAsync(partId);

                ViewBag.CurrentFilter = partition.Name;
                ViewBag.BreadCrumbsLinks = new Tuple<string, string>[] {
                    Tuple.Create("Главная", Url.Action("Index"))
                };
            }

            return View();
        } 
    }
}
