using Microsoft.EntityFrameworkCore;

namespace teamtrack_api.Model;

public class DataContext: DbContext {
    public DataContext(DbContextOptions<DataContext> options): base(options) {}
    public DbSet<Event>? EventList {get; set;}
    public DbSet<Person>? PersonList {get; set;}
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Event>().Property(p => p.Id).HasIdentityOptions(startValue: 1);
        modelBuilder.Entity<Event>().HasData(
            new Event{
                Id = 1,
                Type = "Treening",
                Date = new DateTime(2023, 12, 15, 12, 42, 0, DateTimeKind.Utc),
                Description = "Jooks",
                Location = "Tartu",
                Attendance = true,
            },
            new Event{
                Id = 2,
                Type = "Võistlus",
                Date = new DateTime(2024, 12, 15, 12, 42, 0, DateTimeKind.Utc),
                Description = "Jooks",
                Location = "Tallinn",
                Attendance = false,
            },
            new Event{
                Id = 3,
                Type = "Võistlus",
                Date = new DateTime(2025, 12, 15, 12, 42, 0, DateTimeKind.Utc),
                Description = "Jooks",
                Location = "Pariis",
                Attendance = true,
            },
            new Event{
                Id = 4,
                Type = "Treening",
                Date = new DateTime(2023, 12, 20, 12, 42, 0, DateTimeKind.Utc),
                Description = "Ujumine",
                Location = "Pärnu",
                Attendance = true,
            },
            new Event{
                Id = 5,
                Type = "Võistlus",
                Date = new DateTime(2024, 12, 22, 12, 42, 0, DateTimeKind.Utc),
                Description = "Ujumine",
                Location = "Saaremaa",
                Attendance = true,
            },
            new Event{
                Id = 6,
                Type = "Võistlus",
                Date = new DateTime(2025, 12, 22, 12, 42, 0, DateTimeKind.Utc),
                Description = "Ujumine",
                Location = "Inglismaa",
                Attendance = true,
            },
            new Event{
                Id = 7,
                Type = "Treening",
                Date = new DateTime(2030, 12, 22, 12, 42, 0, DateTimeKind.Utc),
                Description = "Pohlakorjelus",
                Location = "Järvamaa",
                Attendance = true,
            }
        );
    }
}
