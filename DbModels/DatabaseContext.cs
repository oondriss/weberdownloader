using Microsoft.EntityFrameworkCore.Design;

namespace TestApp.DbModels;

public class DatabaseContext : DbContext, IDesignTimeDbContextFactory<DatabaseContext>
{
    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        //Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            _ = optionsBuilder.UseSqlite("Filename=Weber.db");
            _ = optionsBuilder.EnableSensitiveDataLogging();
        }
    }

    public DatabaseContext GetContext()
    {
        return this;
    }

    public DatabaseContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<DatabaseContext> dbContextOptionsBuilder = new();
        _ = dbContextOptionsBuilder.UseSqlite("Filename=Weber.db");
        return new DatabaseContext(dbContextOptionsBuilder.Options);
    }

    public virtual DbSet<Head> Heads { get; set; }
    public virtual DbSet<AdditionalColumn> AdditionalColumns { get; set; }
    public virtual DbSet<AdditionalValue> AdditionalValues { get; set; }
    public virtual DbSet<JobLog> JobLogs { get; set; }
}
