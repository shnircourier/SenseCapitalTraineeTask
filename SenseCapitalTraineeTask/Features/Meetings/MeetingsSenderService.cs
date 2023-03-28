using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace SenseCapitalTraineeTask.Features.Meetings;

/// <summary>
/// Рассылка события удаления мероприятия
/// </summary>
public class MeetingsSenderService
{
    private readonly IModel _chanel;
    private const string QueueName = "EventDeleteEvent";

    /// <summary>
    /// 
    /// </summary>
    public MeetingsSenderService()
    {
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

    /// <summary>
    /// Отправка сообщения
    /// </summary>
    /// <param name="message"></param>
    /// <typeparam name="T"></typeparam>
    public void SendingMessage<T>(T message)
    {
        var jsonString = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(jsonString);
        
        _chanel.BasicPublish("", QueueName, body: body);
    }
}