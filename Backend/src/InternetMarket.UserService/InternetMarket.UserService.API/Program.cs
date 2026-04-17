using InternetMarket.UserService.API.Extensions;
using InternetMarket.UserService.Application.Extensions;
using InternetMarket.UserService.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPresentation(builder.Configuration)
    .AddApplication()
    .AddInfrastructure(builder.Configuration);
builder.Services
    .AddFluentEmail(builder.Configuration);
var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
