using MediportaZadRek.Data.Interfaces;
using MediportaZadRek.Models;

namespace MediportaZadRek.Data
{
    public static class InitialiserExtensions
    {
        public static async Task InitialiseDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var initialiser = scope.ServiceProvider.GetRequiredService<TagsContextInitializer>();

            await initialiser.SeedAsync();
        }
    }

    public class TagsContextInitializer
    {
        private readonly ILogger<TagsContextInitializer> _logger;
        private readonly AppDbContext _context;
        private readonly IThirdPartyApiCollector<List<Tag>> _collector;

        public TagsContextInitializer(ILogger<TagsContextInitializer> logger, AppDbContext context, IThirdPartyApiCollector<List<Tag>> collector)
        {
            _logger = logger;
            _context = context;
            _collector = collector;
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
            }
        }

        public async Task RefreshAsync()
        {
            try
            {
                await TryClearAsync();
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while clearing the database.");
                throw;
            }
        }

        private async Task TrySeedAsync()
        {
            if (!_context.Tags.Any())
            {
                var tags = await _collector.CollectAsync();

                SetPercentagePopulations(tags);

                foreach (var tag in tags)
                {
                    _context.Tags.Add(tag);
                }

                await _context.SaveChangesAsync();
            }
        }
        private async Task TryClearAsync()
        {
            if (_context.Tags.Any())
            {
                _context.Tags.RemoveRange(_context.Tags);
                await _context.SaveChangesAsync();
            }
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
