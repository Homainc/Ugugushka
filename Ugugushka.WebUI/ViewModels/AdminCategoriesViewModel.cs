using Ugugushka.Common.Interfaces;
using Ugugushka.Domain.DtoModels;

namespace Ugugushka.WebUI.ViewModels
{
    public class AdminCategoriesViewModel
    {
        public IPagedResult<CategoryDto> PagedCategories { get; set; }
    }
}
