using Microsoft.EntityFrameworkCore;
using SubmissionArbitraryForm.Domain;
using SubmissionArbitraryForm.Domain.Fields;

namespace SubmissionArbitraryForm.Data;

public class SubmissionsDbContext : DbContext
{
    public DbSet<Submission> Submissions { get; set; }
    public DbSet<FormField> FormFields { get; set; }

    public SubmissionsDbContext(DbContextOptions<SubmissionsDbContext> options) : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "submissions.db");
            optionsBuilder.UseSqlite($"Data Source={path}");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FormField>()
            .HasDiscriminator<string>("FieldType")
            .HasValue<TextField>("Text")
            .HasValue<DropdownField>("Dropdown")
            .HasValue<DateField>("Date")
            .HasValue<RadioField>("Radio")
            .HasValue<CheckboxField>("Checkbox");

        base.OnModelCreating(modelBuilder);
    }
}
