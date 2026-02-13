using InternetMarket.EmailService.Application.Extensions;
using InternetMarket.EmailService.Infrastructure.Extensions.DI;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.Run();