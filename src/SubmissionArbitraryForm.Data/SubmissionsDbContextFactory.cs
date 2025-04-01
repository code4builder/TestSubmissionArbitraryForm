using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SubmissionArbitraryForm.Data;

public class SubmissionsDbContextFactory : IDesignTimeDbContextFactory<SubmissionsDbContext>
{
    public SubmissionsDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SubmissionsDbContext>();
        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "submissions.db");
        optionsBuilder.UseSqlite($"Data Source={path}");

        return new SubmissionsDbContext(optionsBuilder.Options);
    }
}
