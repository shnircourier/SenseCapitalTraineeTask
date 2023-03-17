using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using SenseCapitalTraineeTask.Data;
using SenseCapitalTraineeTask.Data.Entities;
using SenseCapitalTraineeTask.Features.Meetings;
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
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    
    opts.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddSingleton<IRepository<Meeting>, TestDataRepository>();
builder.Services.AddAutoMapper(typeof(MeetingRequestMappingProfile), typeof(MeetingResponseMappingProfile));
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, cfg =>
    {
        cfg.Authority = builder.Configuration["Auth:Authority"];
        cfg.Audience = "MyApi";

        cfg.RequireHttpsMetadata = false;
    });
builder.Services.AddHttpClient();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors(cfg => cfg.AllowAnyOrigin());

app.MapControllers();

app.Run();
