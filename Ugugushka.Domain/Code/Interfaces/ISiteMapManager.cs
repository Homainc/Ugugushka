using System.Threading.Tasks;

namespace Ugugushka.Domain.Code.Interfaces
{
    public interface ISiteMapManager
    {
        Task<string> GetSiteMapDocumentAsync();
    }
}
