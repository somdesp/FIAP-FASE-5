using FIAP.TECH.CORE.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;

namespace FIAP.TECH.INFRASTRUCTURE.Contexts;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Schedule> Schedule { get; set; }

}
