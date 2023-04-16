using Microsoft.EntityFrameworkCore.Storage;

namespace WebLuto.DataContext
{
    public class WLTransaction : IDisposable
    {
        private readonly WLContext _context;
        private IDbContextTransaction _transaction;

        public WLTransaction()
        {
            _context = new WLContext();
            BeginTransaction();
        }

        private async Task BeginTransaction()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task Commit()
        {
            await _transaction.CommitAsync();
        }

        public async Task Rollback()
        {
            await _transaction.RollbackAsync();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
        }
    }
}
