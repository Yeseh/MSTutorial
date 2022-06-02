using System.Text;
using System.Text.Json;
using MSTutorial.PlatformService.Dtos;
using RabbitMQ.Client;

namespace MSTutorial.PlatformService.DataServices.MessageBus;

public class MessageBusClient : IMessageBusClient, IDisposable
{
    private readonly IConfiguration _configuration;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public MessageBusClient(IConfiguration configuration)
    {
       _configuration = configuration;         
       var factory = new ConnectionFactory()
       {
           HostName = _configuration["RabbitMQHost"],
           Port = int.Parse(_configuration["RabbitMQPort"])
       };

       try 
       {
           _connection = factory.CreateConnection();
           _channel = _connection.CreateModel();
           _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
           _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

           Console.WriteLine($"--> Connected to Message Bus");
       }
       catch (Exception ex)
       {
           Console.WriteLine($"--> Could not connect to Message Bus: {ex.Message}");
       }
    }
    public void PublishNewPlatform(PlatformPublishedDto platformPublished)
    {
        var message = JsonSerializer.Serialize(platformPublished);
        if (_connection.IsOpen)
        {
            SendMessage(message);
        }
    }

    private void SendMessage(string message)
    {
        var body = Encoding.UTF8.GetBytes(message);

        _channel.BasicPublish(
            exchange: "trigger", 
            routingKey: "", 
            basicProperties: null, 
            body);
        
        Console.WriteLine($"--> Sent message");
    }

    public void Dispose()
    {
        if (_channel.IsOpen)
        {
            _channel.Close();
            _connection.Close();
        }
    }

    private void RabbitMQ_ConnectionShutdown(object? sender, ShutdownEventArgs e)
    {
           Console.WriteLine($"--> Message bus shutdown");
    }
}