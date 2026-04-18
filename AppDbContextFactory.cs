using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        optionsBuilder.UseSqlServer(
            "Server=LAPTOP-AIVRC8HF\\SQLEXPRESS;Database=JobTracker;Trusted_Connection=True;TrustServerCertificate=True;"
        );

        return new AppDbContext(optionsBuilder.Options);
    }
}