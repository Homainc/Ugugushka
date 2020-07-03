using Ugugushka.Common.Interfaces;
using Ugugushka.Domain.DtoModels;

namespace Ugugushka.WebUI.ViewModels
{
    public class AdminPartitionsViewModel
    {
        public IPagedResult<PartitionDto> PagedPartitions { get; set; }
    }
}
