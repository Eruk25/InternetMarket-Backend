using InternetMarket.EmailService.Infrastructure.Extensions.DI;
using InternetMarket.EmailService.Infrastructure.Extensions.FluentEmail;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddFluentEmail(builder.Configuration);
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();
app.Run();