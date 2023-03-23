using Microsoft.AspNetCore.Authentication.JwtBearer;
using SenseCapitalTraineeTask.Rooms.Data;
using SenseCapitalTraineeTask.Rooms.Data.Entities;
using SenseCapitalTraineeTask.Rooms.Data.MongoDb;
using SenseCapitalTraineeTask.Rooms.Data.Seeds;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IRepository<Room>, MongoDbRoomRepository>();
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
        var roomsRep = scoped.ServiceProvider.GetRequiredService<IRepository<Room>>();

        MongoDbRoomSeeder.Populate(roomsRep);
    });
}

app.UseAuthentication();

app.UseAuthorization();

app.UseCors(cfg => cfg.AllowAnyOrigin());

app.MapControllers();

app.Run();