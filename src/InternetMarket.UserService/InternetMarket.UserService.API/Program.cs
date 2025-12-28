using InternetMarket.UserService.API.Exstensions;
using InternetMarket.UserService.Application.Extensions;
using InternetMarket.UserService.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPresentation(builder.Configuration)
    .AddApplication()
    .AddInfrastructure(builder.Configuration);
var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
