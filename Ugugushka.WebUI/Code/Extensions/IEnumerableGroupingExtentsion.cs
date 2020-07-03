using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using Ugugushka.Domain.DtoModels;

namespace Ugugushka.WebUI.Code.Extensions
{
    public static class IEnumerableGroupingExtentsion
    {
        public static IEnumerable<SelectListItem> ToSelectList(this IEnumerable<IGrouping<PartitionDto, CategoryDto>> grouped)
        {
            var selectList = new List<SelectListItem>();
            foreach (var item in grouped)
            {
                var group = new SelectListGroup { Name = item.Key.Name };
                selectList.AddRange(item.Select(x => new SelectListItem(x.Name, x.Id.ToString()) { Group = group }));
            }
            return selectList;
        }
    }
}
