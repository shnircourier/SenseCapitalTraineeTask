using System.Text;
using System.Text.Json;
using MediatR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SenseCapitalTraineeTask.Features.Meetings.DeleteManyMeetingsByRoomId;

namespace SenseCapitalTraineeTask.Features.Meetings;

public class DeleteRoomListenerService : IHostedService
{
    private readonly IMediator _mediator;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private const string QueueName = "SpaceDeleteEvent";
    
    public DeleteRoomListenerService(IMediator mediator)
    {
        _mediator = mediator;
        var factory = new ConnectionFactory()
        {
            HostName = Environment.GetEnvironmentVariable("RABBITMQ_HOST"),
            UserName = Environment.GetEnvironmentVariable("RABBITMQ_USERNAME"),
            Password = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD"),
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(QueueName, durable: true, exclusive: true);
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (model, eventArgs) =>
        {
            var body = eventArgs.Body.ToArray();

            var message = Encoding.UTF8.GetString(body);

            await ProcessMessageAsync(message, cancellationToken);
        };

        _channel.BasicConsume(QueueName, true, consumer);
        
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _channel.Close();
        _connection.Close();

        return Task.CompletedTask;
    }
    
    private async Task ProcessMessageAsync(string message, CancellationToken cancellationToken)
    {
        var data = JsonSerializer.Deserialize<MeetingEventBody>(message);

        await _mediator.Send(new DeleteManyMeetingsByRoomIdCommand(data!.DeletedId), cancellationToken);
    }
}