using EventStore.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace EventStore.Data;

public class EventDb : DbContext
{
    public EventDb(DbContextOptions options) : base(options) { }
    public DbSet<Event> Events { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Event>().HasData(
            new Event { Id = 1, Name = "Touching Shoulders w/ Dean Chew" });
    }
}