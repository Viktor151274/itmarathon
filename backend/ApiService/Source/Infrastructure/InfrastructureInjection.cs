using System.Diagnostics.CodeAnalysis;
using Epam.ItMarathon.ApiService.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Epam.ItMarathon.ApiService.Infrastructure
{
    /// <summary>
    /// Infrastructure layer injection and setup static class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class InfrastructureInjection
    {
        /// <summary>
        /// Extension method for more fluent setup. This is where all required configuration for Infrastructure layer happens.
        /// </summary>
        public static void InjectInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opts => 
            opts.UseNpgsql(configuration.GetConnectionString("DbConnectionString")));
        }
    }
}
