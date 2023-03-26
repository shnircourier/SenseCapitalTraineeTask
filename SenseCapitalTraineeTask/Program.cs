using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using SenseCapitalTraineeTask.Data;
using SenseCapitalTraineeTask.Data.Entities;
using SenseCapitalTraineeTask.Data.MongoDb;
using SenseCapitalTraineeTask.Data.Seeds;
using SenseCapitalTraineeTask.Features.Meetings;
using SenseCapitalTraineeTask.Identity;
using SenseCapitalTraineeTask.Infrastructure.Middlewares;
using SenseCapitalTraineeTask.Infrastructure.PipelineBehaviors;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opts =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    opts.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

    opts.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "JWT авторизация, необходимо вставить token начиная с ключевого слова Bearer {ваш token}",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    
    opts.OperationFilter<SecurityRequirementsOperationFilter>();
});



//Repositories
builder.Services.AddScoped<IRepository<Meeting>, MongoDbMeetingRepository>();
builder.Services.AddScoped<IRepository<Ticket>, MongoDbTicketRepository>();
builder.Services.AddScoped<IRepository<User>, MongoDbUserRepository>();



//Сервисы
builder.Services.AddAutoMapper(typeof(MeetingRequestMappingProfile), typeof(MeetingResponseMappingProfile));
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddHttpClient();
builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, cfg =>
    {
        cfg.Authority = Environment.GetEnvironmentVariable("ASPNETCORE_IDENTITY_URL") ?? builder.Configuration["Auth:Authority"];
        cfg.Audience = "MyApi";

        cfg.RequireHttpsMetadata = false;
    });
builder.Services.AddScoped<IdentityService>();

builder.Services.AddScoped<RabbitMqSenderService>();
builder.Services.AddHostedService<DeleteImageListenerService>();
builder.Services.AddHostedService<DeleteRoomListenerService>();
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddConsole();
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDataSeeder(() =>
    {
        using var scoped = app.Services.CreateScope();
        var userRep = scoped.ServiceProvider.GetRequiredService<IRepository<User>>();

        MongoDbUserSeeder.Populate(userRep);

    });
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

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
