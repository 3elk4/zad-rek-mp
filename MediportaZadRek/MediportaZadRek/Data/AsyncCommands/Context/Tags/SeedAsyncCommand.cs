using MediportaZadRek.Data.Interfaces;
using MediportaZadRek.Models;
using MediportaZadRek.QCRS.Common.Interfaces;

namespace MediportaZadRek.Data.AsyncCommands.Context.Tags
{
    public class SeedAsyncCommand : IAsyncCommand
    {
        private readonly IDbContext _context;
        private readonly IThirdPartyApiCollector<List<Tag>> _collector;

        public SeedAsyncCommand(IDbContext context, IThirdPartyApiCollector<List<Tag>> collector)
        {
            _context = context;
            _collector = collector;
        }

        public async Task ExecuteAsync()
        {
            if (!_context.Tags.Any())
            {
                var tags = await CollectTagsAsync();
                SetPercentagePopulations(tags);
                await SaveTagsInDatabaseAsync(tags);
            }
        }

        private async Task<List<Tag>> CollectTagsAsync()
        {
            return await _collector.CollectAsync();
        }

        private async Task SaveTagsInDatabaseAsync(List<Tag> tags)
        {
            foreach (var tag in tags)
            {
                _context.Tags.Add(tag);
            }

            await _context.SaveChangesAsync(default);
        }

        private void SetPercentagePopulations(List<Tag> tags)
        {
            decimal totalCount = tags.Select(item => item.Count).Sum();

            tags.ForEach(tag =>
            {
                var percentagePopulation = tag.Count * 100 / totalCount;
                tag.PercentagePopulation = decimal.Round(percentagePopulation, 2);
            });
        }
    }
}
