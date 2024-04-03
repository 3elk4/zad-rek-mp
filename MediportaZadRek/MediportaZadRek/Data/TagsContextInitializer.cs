using MediportaZadRek.Data.AsyncCommands.Context.Tags;
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
                var command = new SeedAsyncCommand(_context, _collector);
                await command.ExecuteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
            }
        }
    }
}
