using System.Diagnostics.CodeAnalysis;
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
        }
    }
}
