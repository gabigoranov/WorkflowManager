using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WorkflowManager.Data;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        
        // Point this to a temporary or local sqlite file for migration generation
        optionsBuilder.UseSqlite($"Data Source=D:\\Projects\\WorkflowManager\\WorkflowManager.db");
        return new ApplicationDbContext(optionsBuilder.Options);
    }
}