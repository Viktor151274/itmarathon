using Epam.ItMarathon.ApiService.Api.Extension;

var builder = WebApplication
    .CreateBuilder(args)
    .ConfigureApplicationBuilder();

var app = builder
    .Build()
    .ConfigureApplication();

app.Run();
