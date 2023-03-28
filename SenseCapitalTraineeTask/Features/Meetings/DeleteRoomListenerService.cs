using System.Text;
using System.Text.Json;
using MediatR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SenseCapitalTraineeTask.Features.Meetings.DeleteManyMeetingsByRoomId;

namespace SenseCapitalTraineeTask.Features.Meetings;

/// <summary>
/// Обработчик события удаления помещения
/// </summary>
public class DeleteRoomListenerService : IHostedService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private const string QueueName = "SpaceDeleteEvent";
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="scopeFactory"></param>
    public DeleteRoomListenerService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
        var factory = new ConnectionFactory
        {
            HostName = Environment.GetEnvironmentVariable("RABBITMQ_HOST"),
            UserName = Environment.GetEnvironmentVariable("RABBITMQ_USERNAME"),
            Password = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD"),
            RequestedHeartbeat = TimeSpan.FromSeconds(30)
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(QueueName, durable: true, exclusive: false);
    }

    /// <inheritdoc />
    public Task StartAsync(CancellationToken cancellationToken)
    {
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (_, eventArgs) =>
        {
            var body = eventArgs.Body.ToArray();

            var message = Encoding.UTF8.GetString(body);

            await ProcessMessageAsync(message, cancellationToken);
        };

        _channel.BasicConsume(QueueName, true, consumer);
        
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task StopAsync(CancellationToken cancellationToken)
    {
        _channel.Close();
        _connection.Close();

        return Task.CompletedTask;
    }
    
    private async Task ProcessMessageAsync(string message, CancellationToken cancellationToken)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var data = JsonSerializer.Deserialize<MeetingEventBody>(message, options);

        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<DeleteImageListenerService>>();
        logger.LogInformation("Получено сообщение: {0}", message);
        await mediator.Send(new DeleteManyMeetingsByRoomIdRequest(data!.DeletedId), cancellationToken);
    }
}