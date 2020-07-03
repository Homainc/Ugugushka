using System.Collections.Generic;
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
    public class CategoryManager : AbstractManager, ICategoryManager
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryManager(ICategoryRepository categoryRepository, ISaveProvider saveProvider, IMapper mapper) :
            base(saveProvider, mapper)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryDto> SaveAsync(CategoryDto categoryDto)
        {
            var category = Mapper.Map<Category>(categoryDto);

            if (category.Id == 0)
                _categoryRepository.SetAdded(category);
            else
            {
                if(!await _categoryRepository.AnyByIdAsync(categoryDto.Id))
                    throw new NotFoundException($"Category with id {categoryDto.Id} not found!");

                _categoryRepository.SetModified(category);
            }

            await SaveChangesAsync();

            return Mapper.Map<CategoryDto>(category);
        }

        public async Task<IEnumerable<IGrouping<PartitionDto, CategoryDto>>> GetAllGroupedByPartitionAsync() =>
            Mapper.Map<IEnumerable<CategoryDto>>(await _categoryRepository.GetAllAsync()).GroupBy(x => x.Partition);

        public async Task<CategoryDto> DeleteAsync(int id)
        {
            var category = await SingleByIdAsync(id);

            _categoryRepository.SetDeleted(category);
            await SaveChangesAsync();

            return Mapper.Map<CategoryDto>(category);
        }

        private async Task<Category> SingleByIdAsync(int id) =>
            await _categoryRepository.SingleByIdOrDefaultAsync(id) ??
            throw new NotFoundException($"Category with {id} not found!");

        public async Task<IPagedResult<CategoryDto>> GetAllPagedAsync(IPageInfo pageInfo) =>
            (await _categoryRepository.GetAllPagedAsync(pageInfo)).Map<Category, CategoryDto>(Mapper);

        public async Task<CategoryDto> GetByIdAsync(int id) => 
            Mapper.Map<CategoryDto>(await SingleByIdAsync(id));

        public async Task<IEnumerable<CategoryDto>> GetAllAsync() =>
            Mapper.Map<IEnumerable<CategoryDto>>(await _categoryRepository.GetAllAsync());
    }
}
