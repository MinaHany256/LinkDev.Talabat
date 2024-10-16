
using LinkDev.Talabat.APIs.Controllers.Errors;
using LinkDev.Talabat.APIs.Extensions;
using LinkDev.Talabat.APIs.Middlewares;
using LinkDev.Talabat.APIs.Services;
using LinkDev.Talabat.Core.Application;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Infrastructure;
using LinkDev.Talabat.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;


namespace LinkDev.Talabat.APIs
{
    public class Program
    {

        public static async Task Main(string[] args)
        {
            

            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services
            // Add services to the container.


            builder.Services
                .AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = false;
                    options.InvalidModelStateResponseFactory = (actionContext) =>
                    {
                        var errors = actionContext.ModelState.Where(P => P.Value!.Errors.Count > 0)
                                      .Select(P => new ApiValidationErrorResponse.ValidationError() 
                                      {
                                          Field = P.Key,
                                          Errors = P.Value!.Errors.Select(E => E.ErrorMessage)
                                      });
                                    
                        return new BadRequestObjectResult(new ApiValidationErrorResponse()
                        {
                            Errors = errors
                        });
                    };
                })
                .AddApplicationPart(typeof(Controllers.AssemblyInformation).Assembly);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddPersistenceServices(builder.Configuration);
            //DependencyInjection.AddPersistenceServices(builder.Services, builder.Configuration);

            //builder.Services.AddScoped(typeof(IHttpContextAccessor), typeof(HttpContextAccessor));
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped(typeof(ILoggedInUserService), typeof(LoggedInUserService));
            builder.Services.AddApplicationServices();

            builder.Services.AddInfrastructureServices(builder.Configuration);

            #endregion

            var app = builder.Build();

            #region DataBase Initialization

            await app.InitializeStoreContextAsync();

            #endregion

            #region Configure Kestrel Middlewares
            // Configure the HTTP request pipeline.

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStatusCodePagesWithReExecute("/Errors/{0}");

            app.UseAuthorization();

            app.UseStaticFiles();

            app.MapControllers();

            #endregion

            app.Run();
        }
    }
}
