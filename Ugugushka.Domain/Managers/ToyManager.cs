using System.Threading.Tasks;
using Ugugushka.Common.Interfaces;
using Ugugushka.Data.Code.Interfaces;
using Ugugushka.Domain.Code.Interfaces;
using Ugugushka.Domain.DtoModels;

namespace Ugugushka.Domain.Managers
{
    internal class ToyManager : IToyManager
    {
        private readonly IToyRepository _toyRepository;
        public ToyManager(IToyRepository toyRepository) => _toyRepository = toyRepository;

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
