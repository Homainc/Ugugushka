using Ugugushka.Common.Interfaces;
using Ugugushka.Domain.DtoModels;

namespace Ugugushka.WebUI.ViewModels
{
    public class AdminToysViewModel
    {
        public IPagedResult<ToyDto> PagedToys { get; set; }
    }
}
