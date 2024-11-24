using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace LinkDev.Talabat.Infrastructure.Persistence.Data.Interceptors
{
    public class AuditInterceptor : SaveChangesInterceptor
    {
        private readonly ILoggedInUserService _loggedInUserService;

        public AuditInterceptor(ILoggedInUserService loggedInUserService)
        {
            _loggedInUserService = loggedInUserService;
        }


        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);
            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        private void UpdateEntities(DbContext? dbContext)
        {
            if (dbContext is null) return;


            var entries = dbContext.ChangeTracker.Entries<IBaseAuditableEntity>()
                 .Where(entry => entry.GetType().IsSubclassOf(typeof(BaseAuditableEntity<>)) && entry is { State: EntityState.Added or EntityState.Modified });

            foreach (var entry in entries)
            {
                var entity = entry.Entity;

                var userId = _loggedInUserService.UserId;
                var currentTime = DateTime.UtcNow;

                if (entry.State == EntityState.Added)
                {
                    SetPropertyIfExists(entity, "CreatedBy", userId!);
                    SetPropertyIfExists(entity, "CreatedOn", currentTime);
                }

                SetPropertyIfExists(entity, "LastModifiedBy", userId!);
                SetPropertyIfExists(entity, "LastModifiedOn", currentTime);

            }

        }

        private void SetPropertyIfExists(object entity, string propertyName, object value)
        {
            var property = entity.GetType().GetProperty(propertyName);
            if (property != null && property.CanWrite)
            {
                property.SetValue(entity, value);
            }
        }

    }
}
