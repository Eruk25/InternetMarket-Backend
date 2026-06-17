using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

var allowedOrigins = builder.Configuration["Cors:AllowedOrigins"];
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyMethod()
              .AllowAnyHeader();

        if (!string.IsNullOrWhiteSpace(allowedOrigins) && allowedOrigins != "*")
            policy.WithOrigins(allowedOrigins.Split(',', StringSplitOptions.TrimEntries));
        else
            policy.AllowAnyOrigin();
    });
});
builder.Services.AddOpenApi();
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        if (exception is null) return;

        var statusCode = exception switch
        {
            ArgumentNullException or ArgumentException => StatusCodes.Status400BadRequest,
            KeyNotFoundException => StatusCodes.Status404NotFound,
            InvalidOperationException => StatusCodes.Status400BadRequest,
            UnauthorizedAccessException => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError,
        };

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/problem+json";

        await context.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Type = $"https://httpstatuses.io/{statusCode}",
            Title = statusCode switch
            {
                StatusCodes.Status400BadRequest => "Bad Request",
                StatusCodes.Status401Unauthorized => "Unauthorized",
                StatusCodes.Status403Forbidden => "Forbidden",
                StatusCodes.Status404NotFound => "Not Found",
                StatusCodes.Status409Conflict => "Conflict",
                StatusCodes.Status422UnprocessableEntity => "Unprocessable Entity",
                _ => "Internal Server Error",
            },
            Status = statusCode,
            Detail = exception.Message,
            Instance = context.Request.Path,
        });
    });
});
app.UseStatusCodePages();

app.Use(async (context, next) =>
{
    if (HttpMethods.IsOptions(context.Request.Method))
    {
        context.Response.StatusCode = 204;
        context.Response.Headers["Access-Control-Allow-Origin"] = "*";
        context.Response.Headers["Access-Control-Allow-Methods"] = "GET, POST, PUT, DELETE, OPTIONS, PATCH";
        context.Response.Headers["Access-Control-Allow-Headers"] = "*";
        return;
    }
    await next();
});

app.UseCors();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

await app.UseOcelot();
app.Run();

