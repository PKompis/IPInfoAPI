using IPInfo.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace IPInfo.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IIPRepository IP { get; }
        Task<int> CommitAsync();
    }
}
