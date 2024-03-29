using MediportaZadRek.Models;
using MediportaZadRek.QCRS.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MediportaZadRek.Data
{
    public class AppDbContext : DbContext, IDbContext
    {
        public DbSet<Tag> Tags { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
