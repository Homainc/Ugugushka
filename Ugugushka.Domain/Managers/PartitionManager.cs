using System.Collections.Generic;
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
    public class PartitionManager : AbstractManager, IPartitionManager
    {
        private readonly IPartitionRepository _partitionRepository;

        public PartitionManager(IPartitionRepository partitionRepository, ISaveProvider saveProvider, IMapper mapper) :
            base(saveProvider, mapper)
        {
            _partitionRepository = partitionRepository;
        }

        public async Task<IPagedResult<PartitionDto>> GetPagedAsync(IPageInfo pageInfo) =>
            (await _partitionRepository.GetPagedAsync(pageInfo)).Map<Partition, PartitionDto>(Mapper);

        public async Task<PartitionDto> SaveAsync(PartitionDto partitionDto)
        {
            var partition = Mapper.Map<Partition>(partitionDto);

            if (partition.Id == 0)
                await _partitionRepository.AddAsync(partition);
            else
                _partitionRepository.Update(partition);

            await SaveChangesAsync();

            return Mapper.Map<PartitionDto>(partition);
        }

        public async Task<PartitionDto> DeleteAsync(int id)
        {
            var partition = await SingleByIdAsync(id);

            _partitionRepository.SetDeleted(partition);
            await SaveChangesAsync();

            return Mapper.Map<PartitionDto>(partition);
        }

        public async Task<PartitionDto> GetByIdAsync(int id) =>
            Mapper.Map<PartitionDto>(await SingleByIdAsync(id));

        private async Task<Partition> SingleByIdAsync(int id) =>
            await _partitionRepository.SingleByIdOrDefaultAsync(id) ??
            throw new NotFoundException($"Partition with id {id} not found!");

        public async Task<IEnumerable<PartitionDto>> GetAllAsync() =>
            Mapper.Map<IEnumerable<PartitionDto>>(await _partitionRepository.GetAllAsync());
    }
}
