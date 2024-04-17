using Microsoft.EntityFrameworkCore;

namespace CalendarApp1.Data;

public class CalendarDbContext : DbContext
{
    public CalendarDbContext(DbContextOptions<CalendarDbContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<Event> Events { get; set; } = default!;
    public DbSet<Day> Days { get; set; } = default!;
    
    public DbSet<Month> Months { get; set; } = default!;

}