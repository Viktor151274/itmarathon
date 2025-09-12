using System.Reflection;
using Epam.ItMarathon.ApiService.Api.Dto.Responses.SystemResponses;
using Epam.ItMarathon.ApiService.Api.Endpoints.Extension.SwaggerTagExtension;

namespace Epam.ItMarathon.ApiService.Api.Endpoints
{
    /// <summary>
    /// Static class containing system endpoints.
    /// </summary>
    public static class SystemEndpoints
    {
        /// <summary>
        /// Registers all system-related endpoints to the app.
        /// </summary>
        /// /// <param name="app">The WebApplication instance.</param>
        public static WebApplication MapSystemEndpoints(this WebApplication app)
        {
            var root = app.MapGroup("/api/system")
                .WithTags("System")
                .WithTagDescription("System", "System wide endpoints")
                .WithOpenApi();

            _ = root.MapGet("/info", GetSystemInfoResponse)
                .Produces<AppInfoResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithSummary("Get application system info.")
                .WithDescription("Returns system app info. Useful for health checks.");

            return app;
        }

        /// <summary>
        /// Returns application system info.
        /// </summary>
        /// <returns>OK with information if the service is running.</returns> 
        public static Task<IResult> GetSystemInfoResponse(IWebHostEnvironment environment)
        {
            return Task.FromResult(Results.Ok(new AppInfoResponse
            {
                DateTime = DateTime.UtcNow,
                Environment = environment.EnvironmentName,
                Build = Assembly.GetExecutingAssembly().GetName().Version
            }));
        }
    }
}
