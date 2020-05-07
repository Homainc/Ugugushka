using System.Threading.Tasks;
using Ugugushka.Common.Interfaces;
using Ugugushka.Domain.Code.Interfaces;
using Ugugushka.Domain.DtoModels;

namespace Ugugushka.Domain.Managers
{
    internal class ToyManager : IToyManager
    {
        public Task<IPagedResult<ToyDto>> GetPagedFilteredAsync(IToyFilterInfo filter, IPageInfo pageInfo)
        {
            throw new System.NotImplementedException();
        }

        public Task<ToyDto> CreateAsync(ToyDto item)
        {
            throw new System.NotImplementedException();
        }
    }
}
