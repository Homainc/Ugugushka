using System.Threading.Tasks;
using Ugugushka.Common.Interfaces;
using Ugugushka.Data.Code.Interfaces;
using Ugugushka.Domain.Code.Abstractions;
using Ugugushka.Domain.Code.Interfaces;
using Ugugushka.Domain.DtoModels;

namespace Ugugushka.Domain.Managers
{
    public class ToyManager : AbstractManager, IToyManager
    {
        private readonly IToyRepository _toyRepository;

        public ToyManager(IToyRepository toyRepository, ISaveProvider saveProvider) : base(saveProvider) =>
            _toyRepository = toyRepository;

        public Task<IPagedResult<ToyDto>> GetPagedFilteredAsync(IToyFilterInfo filter, IPageInfo pageInfo)
        {
            throw new System.NotImplementedException();
        }

        public Task<ToyDto> CreateAsync(ToyCreateDto item)
        {
            throw new System.NotImplementedException();
        }

        public Task<ToyDto> UpdateAsync(ToyUpdateDto item)
        {
            throw new System.NotImplementedException();
        }
    }
}
