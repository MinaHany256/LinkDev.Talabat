using LinkDev.Talabat.Core.Domain.Contracts.Persistence.DbInitializers;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.Talabat.Infrastructure.Persistence._Common
{
    internal abstract class DbInitializer(DbContext _dbContext) : IDbInitializer
    {
        public virtual async Task InitializeAync()
        {
            var PendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();

            if (PendingMigrations.Any())
                await _dbContext.Database.MigrateAsync(); // Update-Database
        }

        public abstract Task SeddAsync();
       
    }
}
