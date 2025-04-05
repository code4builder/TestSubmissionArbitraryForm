using Microsoft.EntityFrameworkCore;
using SubmissionArbitraryForm.Data;
using SubmissionArbitraryForm.Data.Services;
using SubmissionArbitraryForm.Services;
using SubmissionArbitraryForm.Services.Interfaces;

namespace SubmissionArbitraryForm.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Register DbContext
        builder.Services.AddDbContext<SubmissionsDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("SubmissionsDb")));

        builder.Services.AddScoped<SubmissionService>();
        builder.Services.AddScoped<ISubmissionProcessingService, SubmissionProcessingService>();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowVueApp", builder =>
            {
                builder.WithOrigins("http://localhost:50680") // check the port of your Vue app
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<SubmissionsDbContext>();
            dbContext.Database.Migrate(); // apply any pending migrations
        }

        app.UseCors("AllowVueApp");

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
