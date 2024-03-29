using MediatR;
using MediportaZadRek.Data;

namespace MediportaZadRek.QCRS.Tag.Commands
{
    public record RefreshCommand : IRequest
    {
    }

    public class RefreshCommandHandler : IRequestHandler<RefreshCommand>
    {
        private readonly TagsContextInitializer _initializer;

        public RefreshCommandHandler(TagsContextInitializer initializer)
        {
            _initializer = initializer;
        }

        public async Task Handle(RefreshCommand request, CancellationToken cancellationToken)
        {
            await _initializer.RefreshAsync();
        }
    }
}
