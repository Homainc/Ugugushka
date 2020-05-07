using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Ugugushka.Data.Code.Interfaces;

namespace Ugugushka.Data
{
    internal class SaveProvider : ISaveProvider
    {
        private readonly ApplicationContext _db;
        private readonly CancellationToken _cancellationToken;
        public SaveProvider(ApplicationContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _cancellationToken = httpContextAccessor.HttpContext.RequestAborted;
        }

        public async Task SaveAsync() => await _db.SaveChangesAsync(_cancellationToken);
    }
}
