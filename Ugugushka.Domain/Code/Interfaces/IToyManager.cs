using System.Threading.Tasks;
using Ugugushka.Common.Interfaces;
using Ugugushka.Domain.DtoModels;

namespace Ugugushka.Domain.Code.Interfaces
{
    public interface IToyManager
    {
        Task<IPagedResult<ToyDto>> GetPagedFilteredAsync(IToyFilterInfo filter, IPageInfo pageInfo);
        Task<ToyDto> SaveAsync(ToyUpdateDto item);
        Task<ToyDto> DeleteAsync(int id);

        Task<ToyDto> GetByIdAsync(int id);
    }
}
