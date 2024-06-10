using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using MyBlazor.DataAccess.Entities;
using MyBlazor.DataAccess.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using IModel = RabbitMQ.Client.IModel;

namespace RabbitMQ.Consumer;

public class Worker : BackgroundService
{
    private IOrderRepo _orderRepo;
    private ConnectionFactory _factory;
    private IConnection _connection;
    private IModel _channel;

    public Worker(IOrderRepo orderRepo)
    {
        _factory = new ConnectionFactory() { HostName = "localhost" };
        _connection = _factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue:"myQueue", exclusive: false);
        _orderRepo = orderRepo;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (_, ea) =>
        {
            var messageString = Encoding.UTF8.GetString(ea.Body.ToArray());
            var item = JsonSerializer.Deserialize<OrderGetDTO>(messageString);
            var result = await _orderRepo.GetAsync(item.Id);

            var props = _channel.CreateBasicProperties();
            props.ReplyTo = "myQueue";
            props.CorrelationId = ea.BasicProperties.CorrelationId;

            var reply = JsonSerializer.Serialize(result);
            var replyByte = Encoding.UTF8.GetBytes(reply);

            _channel.BasicPublish(
                exchange: "",
                routingKey: "myQueue",
                basicProperties: props,
                body: replyByte);
            
            _channel.BasicAck(ea.DeliveryTag, false);
        };

        _channel.BasicConsume("myQueue", false, consumer);

        return Task.CompletedTask;

    }

    public override void Dispose()
    {
        _channel.Close();
        _connection.Close();
    }
}