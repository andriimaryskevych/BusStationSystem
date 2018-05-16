using Microsoft.Extensions.DependencyInjection;
using BusStationSystem.DAL.EF;
using BusStationSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
namespace BusStationSystem.BLL.Infrastructure
{
    public static class ServiceProviderExtensions
    {
        public static void AddContextService(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationContext>(options =>
               options.UseSqlServer(connectionString));
        }
    }
}
