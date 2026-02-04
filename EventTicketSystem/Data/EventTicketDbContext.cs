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
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // Important for Identity

        // Decimal type
        modelBuilder.Entity<Ticket>()
            .Property(p => p.PricePaid)
            .HasColumnType("decimal(18,2)");

        // Ticket → ApplicationUser
        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.ApplicationUser)
            .WithMany(u => u.Tickets)
            .HasForeignKey(t => t.ApplicationUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}