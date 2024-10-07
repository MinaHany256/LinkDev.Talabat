using LinkDev.Talabat.Core.Domain.Contracts.Persistence;

namespace LinkDev.Talabat.APIs.Extensions
{
    public static class InitializerExtensions
    {
        public static async Task<WebApplication> InitializeStoreContextAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateAsyncScope();
            var services = scope.ServiceProvider;
            var storeContextInitializer = services.GetRequiredService<IStoreContextInitializer>();
            // Ask Runtime Env for an object from "StoreContext" Explicitly.

            var LoggerFactory = services.GetRequiredService<ILoggerFactory>();
            //var Logger = services.GetRequiredService<ILogger<Program>>(); 

            try
            {
                await storeContextInitializer.InitializeAync();
                await storeContextInitializer.SeddAsync();


            }
            catch (Exception ex)
            {
                var Logger = LoggerFactory.CreateLogger<Program>();
                Logger.LogError(ex, "An Error Has Been Occured during applying the Migration. ");
            }

            return app;

        }
    }
}
