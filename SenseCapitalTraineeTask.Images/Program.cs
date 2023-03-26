using Microsoft.AspNetCore.Authentication.JwtBearer;
using SenseCapitalTraineeTask.Images.Features;
using SenseCapitalTraineeTask.Images.Features.Data;
using SenseCapitalTraineeTask.Images.Features.Data.Entities;
using SenseCapitalTraineeTask.Images.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<IRepository<Image>, TestDataRepository>();
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddScoped<ImageSenderService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, cfg =>
    {
        cfg.Authority = Environment.GetEnvironmentVariable("ASPNETCORE_IDENTITY_URL") ?? builder.Configuration["Auth:Authority"];
        cfg.Audience = "MyApi";

        cfg.RequireHttpsMetadata = false;
    });

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddConsole();
});

builder.Services.AddTransient<ExceptionHandlingMiddleware>();

var app = builder.Build();

app.UseCors(cfg =>
{
    cfg.AllowAnyOrigin();
    cfg.AllowAnyHeader();
    cfg.AllowAnyMethod();
});

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();