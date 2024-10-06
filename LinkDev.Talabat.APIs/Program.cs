
using LinkDev.Talabat.Infrastructure.Persistence;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace LinkDev.Talabat.APIs
{
    public class Program
    {

        public static async Task Main(string[] args)
        {
            

            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddPersistenceServices(builder.Configuration);
            //DependencyInjection.AddPersistenceServices(builder.Services, builder.Configuration);

            #endregion

            var app = builder.Build();

            using var scope = app.Services.CreateAsyncScope();
            var services = scope.ServiceProvider;
            var dbContext = services.GetRequiredService<StoreContext>();
            // Ask Runtime Env for an object from "StoreContext" Explicitly.

            var LoggerFactory = services.GetRequiredService<ILoggerFactory>();
            //var Logger = services.GetRequiredService<ILogger<Program>>(); 

            try
            {
                var PendingMigrations = dbContext.Database.GetPendingMigrations();

                if (PendingMigrations.Any())
                    await dbContext.Database.MigrateAsync(); // Update-Database
            }
            catch (Exception ex)
            {
                 var Logger = LoggerFactory.CreateLogger<Program>();
                Logger.LogError(ex, "An Error Has Been Occured during applying the Migration. ");
            }

            #region Configure Kestrel Middlewares
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            #endregion

            app.Run();
        }
    }
}
