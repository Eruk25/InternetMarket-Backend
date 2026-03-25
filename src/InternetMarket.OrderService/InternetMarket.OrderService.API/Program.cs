using InternetMarket.OrderService.API.Extensions;
using InternetMarket.OrderService.Application.Extensions;
using InternetMarket.OrderService.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddPresentation(builder.Configuration)
    .AddApplication()
    .AddInfrastructure(builder.Configuration);
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapControllers();
app.UseHttpsRedirection();
app.Run();
