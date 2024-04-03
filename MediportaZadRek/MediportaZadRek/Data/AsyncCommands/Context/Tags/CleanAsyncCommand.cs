using MediportaZadRek.Data.Interfaces;
using MediportaZadRek.QCRS.Common.Interfaces;

namespace MediportaZadRek.Data.AsyncCommands.Context.Tags
{
    public class CleanAsyncCommand : IAsyncCommand
    {
        private readonly IDbContext _context;

        public CleanAsyncCommand(IDbContext context)
        {
            _context = context;
        }
        public async Task ExecuteAsync()
        {
            if (_context.Tags.Any())
            {
                _context.Tags.RemoveRange(_context.Tags);
                await _context.SaveChangesAsync(default);
            }
        }
    }
}
