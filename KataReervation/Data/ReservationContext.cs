using KataReservation.Models;
using Microsoft.EntityFrameworkCore;

public class ReservationContext : DbContext
{
    public ReservationContext(DbContextOptions<ReservationContext> options) : base(options)
    {
    }

    public DbSet<Salle> Salles { get; set; }
    public DbSet<Reservation> Reservations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure the relationship
        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Salle)
            .WithMany(s => s.Reservations);

        // Seed data
        modelBuilder.Entity<Salle>().HasData(
            new Salle { Id = "1", Nom = "S1" },
            new Salle { Id = "2", Nom = "S2" },
            new Salle { Id = "3", Nom = "S3" }
        );
    }
}
