using LinkDev.Talabat.Core.Application.Abstraction.Models.Auth;
using LinkDev.Talabat.Core.Domain.Entites.Identity;
using LinkDev.Talabat.Infrastructure.Persistence._Identity;
using Microsoft.AspNetCore.Identity;

namespace LinkDev.Talabat.APIs.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            services.AddIdentity<ApplicationUser, IdentityRole>((IdentityOptions) =>
            {
                IdentityOptions.SignIn.RequireConfirmedAccount = true;
                IdentityOptions.SignIn.RequireConfirmedEmail = true;
                IdentityOptions.SignIn.RequireConfirmedPhoneNumber = true;

            })
                .AddEntityFrameworkStores<StoreIdentityDbContext>();

            return services;
        }
    }
}
