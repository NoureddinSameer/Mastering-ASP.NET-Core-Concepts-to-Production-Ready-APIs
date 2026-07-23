using M02.BaselineAPIProjectMinimal.Data.Configurations;
using M02.BaselineAPIProjectMinimal.Entities;
using Microsoft.EntityFrameworkCore;

namespace M02.BaselineAPIProjectMinimal.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<ProjectTask> ProjectTasks => Set<ProjectTask>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProjectConfiguration).Assembly);
    }
}