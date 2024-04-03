using MediatR;
using MediportaZadRek.Data.AsyncCommands.Context.Tags;
using MediportaZadRek.Data.Interfaces;
using MediportaZadRek.Models;
using MediportaZadRek.QCRS.Common.Interfaces;

namespace MediportaZadRek.QCRS.Tags.Commands
{
    public record RefreshCommand : IRequest
    {
    }

    public class RefreshCommandHandler : IRequestHandler<RefreshCommand>
    {
        private readonly IDbContext _context;
        private readonly IThirdPartyApiCollector<List<Tag>> _collector;

        public RefreshCommandHandler(IDbContext context, IThirdPartyApiCollector<List<Tag>> collector)
        {
            _context = context;
            _collector = collector;
        }

        public async Task Handle(RefreshCommand request, CancellationToken cancellationToken)
        {
            var cleanAsyncCommand = new CleanAsyncCommand(_context);
            var seedAsyncCommand = new SeedAsyncCommand(_context, _collector);

            await cleanAsyncCommand.ExecuteAsync();
            await seedAsyncCommand.ExecuteAsync();
        }
    }
}
