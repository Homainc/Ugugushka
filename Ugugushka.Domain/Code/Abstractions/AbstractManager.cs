using System.Threading.Tasks;
using Ugugushka.Data.Code.Interfaces;

namespace Ugugushka.Domain.Code.Abstractions
{
    public class AbstractManager
    {
        private readonly ISaveProvider _saveProvider;
        public AbstractManager(ISaveProvider saveProvider) => _saveProvider = saveProvider;
        protected async Task SaveChangesAsync() => await _saveProvider.SaveAsync();
    }
}
