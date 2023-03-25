using Microsoft.AspNetCore.Authentication.JwtBearer;
using SenseCapitalTraineeTask.Rooms.Data;
using SenseCapitalTraineeTask.Rooms.Data.Entities;
using SenseCapitalTraineeTask.Rooms.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<IRepository<Room>, TestDataRepository>();
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddScoped<RabbitMqSenderService>();

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

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors(cfg => cfg.AllowAnyOrigin());

app.MapControllers();

app.Run();