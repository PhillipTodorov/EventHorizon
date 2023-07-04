using EventHorizonBackend.Models;
using EventHorizonBackend.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace EventHorizonBackend.Data
{
    public class EventHorizonDbContext : IdentityDbContext<IdentityUser>
    {
        public EventHorizonDbContext(DbContextOptions options) : base(options)
        {
        }

        //public DbSet<BlogPost> BlogPosts { get; set; }
        //public DbSet<Tag> Tags { get; set; }
        public DbSet<UserEvent> UserEvents { get; set; }
        public DbSet<Event> Events { get; set; }


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
    }
}
