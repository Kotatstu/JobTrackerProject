using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    //Declare the models
    public DbSet<JobApplication> JobApplications {get; set;}

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //Ensure enum stored as string
        modelBuilder.Entity<JobApplication>()
            .Property(j => j.Status)
            .HasConversion<string>();
    }
}