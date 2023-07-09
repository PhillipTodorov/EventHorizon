using EventHorizonBackend.Models;
using EventHorizonBackend.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace EventHorizonBackend.Data
{
    public class EventHorizonDbContext : IdentityDbContext<IdentityUser>, IEventHorizonDbContext
    {
        public EventHorizonDbContext(DbContextOptions options) : base(options)
        {
        }
        public virtual DbSet<UserEvent> UserEvents { get; set; }
        public virtual DbSet<Event> Events { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure UserEvent as a many-to-many relationship
            modelBuilder.Entity<UserEvent>()
                .HasKey(ue => new { ue.UserId, ue.EventId }); // Composite key

            modelBuilder.Entity<UserEvent>()
                .HasOne(ue => ue.User)
                .WithMany(u => u.UserEvents)
                .HasForeignKey(ue => ue.UserId);

            modelBuilder.Entity<UserEvent>()
                .HasOne(ue => ue.Event)
                .WithMany(e => e.UserEvents)
                .HasForeignKey(ue => ue.EventId);
        }
        public void SetModifiedState(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }
        public new async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
