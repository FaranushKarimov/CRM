using contracts;
using Microsoft.Extensions.DependencyInjection;
using loggerService;
using Microsoft.AspNetCore.Builder;
using entities.DataContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using contracts.Services;
using services;

namespace CRM.Extensions
{
    public static class ServiceRegistrations
    {
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options => options.AddPolicy("CRMPolicy", builder =>
                builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()));

        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<DataContext>(optionsAction =>
                optionsAction.UseNpgsql(configuration.GetConnectionString("Default"), ma => ma.MigrationsAssembly("CRM")).UseLazyLoadingProxies(false));
        public static void ConfigureLoggerService(this IServiceCollection services) =>
           services.AddScoped<ILoggerManager, LoggerManager>();

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options =>
            {

            });

        public static void ConfigureDIs(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
           
        }
    }
}
