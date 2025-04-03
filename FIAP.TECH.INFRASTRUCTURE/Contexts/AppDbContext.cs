﻿using FIAP.TECH.CORE.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;

namespace FIAP.TECH.INFRASTRUCTURE.Contexts;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    public DbSet<Patient> Users { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Region> Regions { get; set; }

}
