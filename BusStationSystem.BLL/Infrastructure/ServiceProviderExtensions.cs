using Microsoft.Extensions.DependencyInjection;
using BusStationSystem.DAL.EF;
using BusStationSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using BusStationSystem.DAL.Interfaces;
using BusStationSystem.DAL.Repositories;

namespace BusStationSystem.BLL.Infrastructure
{
    public static class ServiceProviderExtensions
    {
        public static void AddContextService(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationContext>(options =>
               options.UseSqlServer(connectionString));

            services.AddDbContext<ApplicationIdentityContext>(options =>
                 options.UseSqlServer(connectionString));
        }

        public static void AddIdentityService(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
            })
               .AddEntityFrameworkStores<ApplicationIdentityContext>();
        }

        public static void AddUnitOfWorkService(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
