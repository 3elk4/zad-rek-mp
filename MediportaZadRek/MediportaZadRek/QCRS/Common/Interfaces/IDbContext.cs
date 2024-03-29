using Microsoft.EntityFrameworkCore;

namespace MediportaZadRek.QCRS.Common.Interfaces
{
    public interface IDbContext
    {
        public DbSet<Models.Tag> Tags { get; set; }
    }
}
