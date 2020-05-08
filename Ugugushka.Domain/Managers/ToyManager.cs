using System.Threading.Tasks;
using AutoMapper;
using Ugugushka.Common.Interfaces;
using Ugugushka.Data.Code.Interfaces;
using Ugugushka.Data.Models;
using Ugugushka.Domain.Code.Abstractions;
using Ugugushka.Domain.Code.Extensions;
using Ugugushka.Domain.Code.Interfaces;
using Ugugushka.Domain.DtoModels;

namespace Ugugushka.Domain.Managers
{
    public class ToyManager : AbstractManager, IToyManager
    {
        private readonly IToyRepository _toyRepository;

        public ToyManager(IToyRepository toyRepository, ISaveProvider saveProvider, IMapper mapper) : base(saveProvider, mapper) =>
            _toyRepository = toyRepository;

        public async Task<IPagedResult<ToyDto>> GetPagedFilteredAsync(IToyFilterInfo filter, IPageInfo pageInfo) =>
            (await _toyRepository.GetFilteredPagedAsync(filter, pageInfo)).Map<Toy, ToyDto>(Mapper);

        public Task<ToyDto> CreateAsync(ToyCreateDto item)
        {
            throw new System.NotImplementedException();
        }

        public Task<ToyDto> UpdateAsync(ToyUpdateDto item)
        {
            throw new System.NotImplementedException();
        }

        public Task<ToyDto> DeleteAsync(uint id)
        {
            throw new System.NotImplementedException();
        }
    }
}
