using Ugugushka.Common.Interfaces;
using Ugugushka.Domain.DtoModels;

namespace Ugugushka.WebUI.ViewModels
{
    public class HomePageViewModel
    {
        public IPagedResult<ToyDto> PagedToys { get; }
    }
}
