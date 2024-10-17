﻿using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Infrastructure.Persistence._Identity;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using LinkDev.Talabat.Infrastructure.Persistence.Data.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
 
namespace LinkDev.Talabat.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region Store DbContext
            services.AddDbContext<StoreDbContext>((optionsBuilder) =>
                {
                    optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseSqlServer(configuration.GetConnectionString("StoreContext"));
                }/*, contextLifetime: ServiceLifetime.Scoped , optionsLifetime: ServiceLifetime.Scoped*/);

            services.AddScoped<IStoreContextInitializer, StoreDbContextInitializer>();

            services.AddScoped(typeof(ISaveChangesInterceptor), typeof(CustomSaveChangesInterceptor));
            #endregion

            #region Identity DbContext

            services.AddDbContext<StoreIdentityDbContext>((optionsBuilder) =>
            {
                optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(configuration.GetConnectionString("IdentityContext"));
            }/*, contextLifetime: ServiceLifetime.Scoped , optionsLifetime: ServiceLifetime.Scoped*/);

            #endregion

            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork.UnitOfWork));
            return services;
        }
    }
}
