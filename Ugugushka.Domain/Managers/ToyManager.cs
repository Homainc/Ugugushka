using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ugugushka.Common.Interfaces;
using Ugugushka.Data.Code.Interfaces;
using Ugugushka.Data.Models;
using Ugugushka.Domain.Code.Abstractions;
using Ugugushka.Domain.Code.Exceptions;
using Ugugushka.Domain.Code.Extensions;
using Ugugushka.Domain.Code.Interfaces;
using Ugugushka.Domain.DtoModels;

namespace Ugugushka.Domain.Managers
{
    public class ToyManager : AbstractManager, IToyManager
    {
        private readonly IToyRepository _toyRepository;
        private readonly IPictureManager _pictureManager;

        public ToyManager(IToyRepository toyRepository, IPictureManager pictureManager, ISaveProvider saveProvider,
            IMapper mapper) : base(saveProvider, mapper)
        {
            _toyRepository = toyRepository;
            _pictureManager = pictureManager;
        }

        public async Task<IPagedResult<ToyDto>> GetPagedFilteredAsync(IToyFilterInfo filter, IPageInfo pageInfo) =>
            (await _toyRepository.GetFilteredPagedAsync(filter, pageInfo)).Map<Toy, ToyDto>(Mapper);

        public async Task<ToyDto> GetByIdAsync(int id) =>
            Mapper.Map<Toy, ToyDto>(await _toyRepository.SingleOrDefaultByIdEagerAsync(id)) ??
            throw new NotFoundException($"Toy with id {id} not found!");

        public async Task<ToyDto> CreateAsync(ToyCreateDto item)
        {
            var toy = Mapper.Map<ToyCreateDto, Toy>(item);
            
            _toyRepository.Create(Mapper.Map<ToyCreateDto, Toy>(item));
            await SaveChangesAsync();

            return Mapper.Map<Toy, ToyDto>(toy);
        }

        public Task<ToyDto> UpdateAsync(ToyUpdateDto item)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ToyDto> DeleteAsync(int id)
        { 
            var toy = await _toyRepository.SingleOrDefaultByIdEagerAsync(id) ?? 
                      throw new NotFoundException($"Toy with id {id} not found!");

            _toyRepository.Delete(toy);
            await _pictureManager.DeletePictureAsync(toy.Images.Select(x => x.PublicId).ToList());
            
            await SaveChangesAsync();
            return Mapper.Map<Toy, ToyDto>(toy);
        }
    }
}
