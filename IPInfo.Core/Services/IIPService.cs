using IPInfo.Library.Interfaces;
using System.Threading.Tasks;

namespace IPInfo.Core.Services
{
    public interface IIPService
    {
        Task<IPDetails> GetIPDetails(string ip);
    }
}
