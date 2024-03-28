using MediportaZadRek.Models;
using Microsoft.EntityFrameworkCore;

namespace MediportaZadRek.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Tag> Tags { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
