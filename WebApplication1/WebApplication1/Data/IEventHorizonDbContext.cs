using EventHorizonBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace EventHorizonBackend.Data
{
    public interface IEventHorizonDbContext
    {
        DbSet<Event> Events { get; set; }
        DbSet<UserEvent> UserEvents { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        void SetModifiedState(object entity);

    }
}