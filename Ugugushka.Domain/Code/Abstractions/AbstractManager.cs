using System.Threading.Tasks;
using AutoMapper;
using Ugugushka.Data.Code.Interfaces;

namespace Ugugushka.Domain.Code.Abstractions
{
    public class AbstractManager
    {
        private readonly ISaveProvider _saveProvider;
        protected readonly IMapper Mapper;

        public AbstractManager(ISaveProvider saveProvider, IMapper mapper)
        {
            Mapper = mapper;
            _saveProvider = saveProvider;
        }

        protected async Task SaveChangesAsync() => await _saveProvider.SaveAsync();
    }
}
