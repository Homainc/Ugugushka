using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ugugushka.Common.Interfaces;
using Ugugushka.Data.Code.Interfaces;
using Ugugushka.Data.Models;
using Ugugushka.Domain.Code.Abstractions;
using Ugugushka.Domain.Code.Constants;
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
        private readonly IToyImageRepository _toyImageRepository;

        public ToyManager(IToyRepository toyRepository, IPictureManager pictureManager, IToyImageRepository toyImageRepository, ISaveProvider saveProvider,
            IMapper mapper) : base(saveProvider, mapper)
        {
            _toyRepository = toyRepository;
            _pictureManager = pictureManager;
            _toyImageRepository = toyImageRepository;
        }

        public async Task<IPagedResult<ToyDto>> GetPagedFilteredAsync(IToyFilterInfo filter, IPageInfo pageInfo) =>
            (await _toyRepository.GetFilteredPagedAsync(filter, pageInfo)).Map<Toy, ToyDto>(Mapper);

        public async Task<ToyDto> GetByIdAsync(int id) => 
            Mapper.Map<ToyDto>(await SingleByIdAsync(id));

        public async Task<ToyDto> SaveAsync(ToyUpdateDto item)
        {
            var newToy = Mapper.Map<ToyUpdateDto, Toy>(item);

            if (item.Id != 0)
            {
                var oldToy = await SingleByIdAsync(item.Id);
                var oldImageIds = oldToy.Images.Select(x => x.PublicId).ToHashSet();

                // Changing tag for relevant pictures to 'toy'
                var newImageIds = newToy.Images.Select(x => x.PublicId).ToHashSet();
                newImageIds.ExceptWith(oldImageIds);
                await _pictureManager.ChangePictureTagAsync(
                    newImageIds.ToList(),
                    CloudinaryTagDefaults.Toy);
                // Setting state for relevant to 'added' and other to 'modified'
                foreach (var img in newToy.Images)
                {
                    if (newImageIds.Contains(img.PublicId))
                        _toyImageRepository.SetAdded(img);
                    else
                        _toyImageRepository.SetModified(img);
                }


                // Changing tag for irrelevant pictures to 'temp'
                newImageIds = newToy.Images.Select(x => x.PublicId).ToHashSet();
                oldImageIds.ExceptWith(newImageIds);
                await _pictureManager.ChangePictureTagAsync(
                    oldImageIds.ToList(),
                    CloudinaryTagDefaults.Temp);
                // Setting state for irrelevant pictures to 'deleted' 
                _toyImageRepository.RemoveRange(oldToy.Images.Where(x => oldImageIds.Contains(x.PublicId)));

                _toyRepository.SetModified(newToy);
            }
            else
            {
                await _pictureManager.ChangePictureTagAsync(
                    newToy.Images.Select(x => x.PublicId).ToList(), 
                    CloudinaryTagDefaults.Toy);

                await _toyRepository.AddAsync(newToy);
            }

            await _pictureManager.DeleteTemporaryPicturesAsync();
            await SaveChangesAsync();

            return Mapper.Map<ToyDto>(newToy);
        }

        public async Task<ToyDto> DeleteAsync(int id)
        {
            var toy = await SingleByIdAsync(id);

            _toyRepository.SetDeleted(toy);
            await _pictureManager.DeletePictureAsync(toy.Images.Select(x => x.PublicId).ToList());
            
            await SaveChangesAsync();
            return Mapper.Map<Toy, ToyDto>(toy);
        }

        private async Task<Toy> SingleByIdAsync(int id) =>
            await _toyRepository.SingleOrDefaultByIdEagerAsync(id) ??
            throw new NotFoundException($"Toy with id {id} not found!");
    }
}
