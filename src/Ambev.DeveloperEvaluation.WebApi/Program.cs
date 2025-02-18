using Ambev.DeveloperEvaluation.Application;
using Ambev.DeveloperEvaluation.Common.HealthChecks;
using Ambev.DeveloperEvaluation.Common.Logging;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.IoC;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.WebApi.Middleware;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Ambev.DeveloperEvaluation.WebApi;

public class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            Log.Information("Starting web application");

            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            builder.AddDefaultLogging();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.AddBasicHealthChecks();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<DefaultContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("Ambev.DeveloperEvaluation.ORM")
                )
            );

            builder.Services.AddJwtAuthentication(builder.Configuration);

            builder.RegisterDependencies();

            builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(ApplicationLayer).Assembly);

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(
                    typeof(ApplicationLayer).Assembly,
                    typeof(Program).Assembly
                );
            });

            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            var app = builder.Build();
            app.UseMiddleware<ValidationExceptionMiddleware>();

            // Check and apply pending migrations
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DefaultContext>();
                var maxRetries = 5;
                var retryCount = 0;
                var connected = false;

                while (!connected && retryCount < maxRetries)
                {
                    try
                    {
                        Log.Information("Attempting to connect to database... Attempt {RetryCount}", retryCount + 1);
                        
                        // Check for pending migrations
                        var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
                        var pendingMigrationsList = pendingMigrations.ToList();
                        
                        if (pendingMigrationsList.Any())
                        {
                            Log.Information("Found {Count} pending migrations. Applying...", pendingMigrationsList.Count);
                            foreach (var migration in pendingMigrationsList)
                            {
                                Log.Information("Applying migration: {Migration}", migration);
                            }
                            await dbContext.Database.MigrateAsync();
                            Log.Information("Migrations applied successfully");
                        }
                        else
                        {
                            Log.Information("No pending migrations found");
                        }

                        connected = true;
                        Log.Information("Database connection established successfully");
                    }
                    catch (Exception ex)
                    {
                        retryCount++;
                        Log.Warning(ex, "Failed to connect to database. Attempt {RetryCount} of {MaxRetries}", retryCount, maxRetries);
                        if (retryCount < maxRetries)
                        {
                            await Task.Delay(5000); // Wait 5 seconds before retrying
                        }
                        else
                        {
                            Log.Error(ex, "Could not establish database connection after {MaxRetries} attempts", maxRetries);
                            throw;
                        }
                    }
                }
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseBasicHealthChecks();

            app.MapControllers();

            await app.RunAsync();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
            throw;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
