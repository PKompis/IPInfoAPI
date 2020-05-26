using IPInfo.Core;
using IPInfo.Core.Repositories;
using IPInfo.Data.Repositories;
using System.Threading.Tasks;

namespace IPInfo.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IPInfoDbContext _context;
        private IPRepository _ipRepository;

        public UnitOfWork(IPInfoDbContext context)
        {
            this._context = context;
        }

        public IIPRepository IP => _ipRepository = _ipRepository ?? new IPRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
