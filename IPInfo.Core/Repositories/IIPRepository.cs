using IPInfo.Core.Models;
using System.Threading.Tasks;

namespace IPInfo.Core.Repositories
{
    public interface IIPRepository : IRepository<IP>
    {
        Task<IP> GetIPDetailsAsync(string ip);
    }
}
