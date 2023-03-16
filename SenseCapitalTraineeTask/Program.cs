using FluentValidation;
using MediatR;
using SenseCapitalTraineeTask.Data;
using SenseCapitalTraineeTask.Data.Entities;
using SenseCapitalTraineeTask.Features.Meetings;
using SenseCapitalTraineeTask.Infrastructure.Middlewares;
using SenseCapitalTraineeTask.Infrastructure.PipelineBehaviors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IRepository<Meeting>, TestDataRepository>();
builder.Services.AddAutoMapper(typeof(MeetingRequestMappingProfile), typeof(MeetingResponseMappingProfile));
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();