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
    public DbSet<EventTicket> EventTickets { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // Important for Identity

        // Decimal type
        modelBuilder.Entity<EventTicket>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18,2)");

        // EventTicket → Event
        modelBuilder.Entity<EventTicket>()
            .HasOne(et => et.Event)
            .WithMany(e => e.TicketTypes)
            .HasForeignKey(et => et.EventId);

        // Ticket → EventTicket
        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.EventTicket)
            .WithMany()
            .HasForeignKey(t => t.EventTicketId)
            .OnDelete(DeleteBehavior.Restrict);

        // Ticket → ApplicationUser
        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.ApplicationUser)
            .WithMany(u => u.Tickets)
            .HasForeignKey(t => t.ApplicationUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}