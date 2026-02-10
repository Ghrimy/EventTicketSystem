using EventTicketSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventTicketSystem.Data;

public class EventTicketDbContext : IdentityDbContext<ApplicationUser>
{
    public EventTicketDbContext(DbContextOptions<EventTicketDbContext> options)
        : base(options)
    {
    }
    public DbSet<Event> Events { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // Important for Identity

        // Decimal type
        modelBuilder.Entity<Ticket>()
            .Property(p => p.PricePaid)
            .HasColumnType("decimal(18,2)");
        
        modelBuilder.Entity<Event>()
            .Property(p => p.TicketPrice)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Event>()
            .Property(p => p.RowVersion)
            .IsRowVersion();

        // Ticket → ApplicationUser
        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.ApplicationUser)
            .WithMany(u => u.Tickets)
            .HasForeignKey(t => t.ApplicationUserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Event)
            .WithMany(e => e.Tickets)
            .HasForeignKey(t => t.EventId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}