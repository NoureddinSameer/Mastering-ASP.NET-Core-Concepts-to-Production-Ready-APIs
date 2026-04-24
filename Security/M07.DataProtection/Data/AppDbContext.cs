using M07.DataProtection.Data.Configurations;
using M07.DataProtection.Entities;
using Microsoft.EntityFrameworkCore;

namespace M07.DataProtection.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Bid> Bids => Set<Bid>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BidConfiguration).Assembly);
    }
}