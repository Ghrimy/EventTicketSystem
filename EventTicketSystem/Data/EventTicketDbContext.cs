using EventTicketSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace EventTicketSystem.Data;

public class EventTicketDbContext(DbContextOptions<EventTicketDbContext> options) : DbContext(options)
{
    public DbSet<Event> Events { get; set; }
    public DbSet<EventTicket> EventTickets { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Property types
        modelBuilder.Entity<EventTicket>().Property(p => p.Price).HasColumnType("decimal(18,2)");
        
        //Relationships
        modelBuilder.Entity<EventTicket>()
            .HasOne(et => et.Event)
            .WithMany(e => e.TicketTypes)
            .HasForeignKey(et => et.EventId);
    }
}