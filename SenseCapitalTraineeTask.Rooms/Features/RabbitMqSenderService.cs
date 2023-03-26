using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace SenseCapitalTraineeTask.Rooms.Features;

public class RabbitMqSenderService
{
    private readonly ILogger<RabbitMqSenderService> _logger;
    private readonly IModel _chanel;
    private const string QueueName = "SpaceDeleteEvent";

    public RabbitMqSenderService(ILogger<RabbitMqSenderService> logger)
    {
        _logger = logger;
        var factory = new ConnectionFactory
        {
            HostName = Environment.GetEnvironmentVariable("RABBITMQ_HOST"),
            UserName = Environment.GetEnvironmentVariable("RABBITMQ_USERNAME"),
            Password = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD")
        };

        var connection = factory.CreateConnection();
        _chanel = connection.CreateModel();
        _chanel.QueueDeclare(QueueName, durable: true, exclusive: false);
    }

    public void SendingMessage<T>(T message)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var jsonString = JsonSerializer.Serialize(message, options);
        var body = Encoding.UTF8.GetBytes(jsonString);
        
        _logger.LogInformation("Отправка сообщения: {0}", jsonString);
        
        _chanel.BasicPublish("", QueueName, body: body);
    }
}