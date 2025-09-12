using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Epam.ItMarathon.ApiService.Api.Endpoints;
using Epam.ItMarathon.ApiService.Infrastructure.Database;
using Serilog;

namespace Epam.ItMarathon.ApiService.Api.Extension
{
    /// <summary>
    /// WebApplication builder static setup-class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class WebApplicationExtensions
    {
        /// <summary>
        /// Extension method for more fluent setup. This is where all required configuration happens.
        /// </summary>
        /// <param name="app">The WebApplication instance.</param>
        public static WebApplication ConfigureApplication(this WebApplication app)
        {
            #region Logging

            _ = app.UseSerilogRequestLogging();

            #endregion Logging

            #region Security

            _ = app.UseHsts();
            _ = app.UseHttpsRedirection();

            #endregion Security

            #region API Configuration

            _ = app.UseHttpsRedirection();

            #endregion API Configuration

            #region Swagger
            var textInfo = CultureInfo.CurrentCulture.TextInfo;

            _ = app.UseSwagger();
            _ = app.UseSwaggerUI(c =>
                c.SwaggerEndpoint(
                    "/swagger/v1/swagger.json",
                    $"Mycolaychik API - {textInfo.ToTitleCase(app.Environment.EnvironmentName)} - V1"));

            #endregion Swagger

            #region MinimalApi

            _ = app.MapSystemEndpoints();
            _ = app.MapRoomEndpoints();

            #endregion MinimalApi

            app.Services.MigrateDatabase();

            return app;
        }
    }
}
