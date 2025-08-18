using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Epam.ItMarathon.ApiService.Api.Endpoints;

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
            #region Security

            _ = app.UseHsts();

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

            #endregion MinimalApi

            return app;
        }
    }
}
