using InternetMarket.OrderService.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapControllers();
app.UseHttpsRedirection();
app.Run();
