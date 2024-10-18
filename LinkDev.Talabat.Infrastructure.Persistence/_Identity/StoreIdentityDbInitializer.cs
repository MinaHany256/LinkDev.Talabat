﻿using LinkDev.Talabat.Core.Domain.Contracts.Persistence.DbInitializers;
using LinkDev.Talabat.Core.Domain.Entites.Identity;
using LinkDev.Talabat.Infrastructure.Persistence._Common;
using Microsoft.AspNetCore.Identity;

namespace LinkDev.Talabat.Infrastructure.Persistence._Identity
{
    internal sealed class StoreIdentityDbInitializer(StoreIdentityDbContext _dbContext, UserManager<ApplicationUser> _userManager) : DbInitializer(_dbContext), IStoreIdentityDbInitializer
    {

        public override async Task SeddAsync()
        {
            var user = new ApplicationUser()
            {
                DisplayName = "Mina Hany",
                UserName = "Mina.hany",
                Email = "mina.hany@gmail.com",
                PhoneNumber = "01211370223"
            };

            await _userManager.CreateAsync(user, "P@ssw0rd");

        }
    }
}