using InternetMarket.EmailService.Application.Extensions;
using InternetMarket.EmailService.Infrastructure.Extensions.DI;
using InternetMarket.EmailService.Infrastructure.Extensions.FluentEmail;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);
builder.Services
    .AddFluentEmail(builder.Configuration);
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.Run();