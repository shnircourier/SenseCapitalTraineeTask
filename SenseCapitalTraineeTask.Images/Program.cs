using Microsoft.AspNetCore.Authentication.JwtBearer;
using SenseCapitalTraineeTask.Images.Data;
using SenseCapitalTraineeTask.Images.Data.Entities;
using SenseCapitalTraineeTask.Images.Data.MongoDb;
using SenseCapitalTraineeTask.Images.Data.Seeds;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IRepository<Image>, MongoDbImageRepository>();
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, cfg =>
    {
        cfg.Authority = builder.Configuration["Auth:Authority"];
        cfg.Audience = "MyApi";

        cfg.RequireHttpsMetadata = false;
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDataSeeder(() =>
    {
        using var scoped = app.Services.CreateScope();
        var roomsRep = scoped.ServiceProvider.GetRequiredService<IRepository<Image>>();

        MongoDbImageSeeder.Populate(roomsRep);
    });
}

app.UseAuthentication();

app.UseAuthorization();

app.UseCors(cfg => cfg.AllowAnyOrigin());

app.MapControllers();

app.Run();