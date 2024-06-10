using System.Collections.Concurrent;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MyBlazor.Server.RabbitMQPublisher;

public class OrderPublisher
{
    private ConnectionFactory _factory;
    private IConnection _connection;
    private IModel _channel;
    private ConcurrentDictionary<string, TaskCompletionSource<object>> _mapper;

    public OrderPublisher()
    {
        _factory = new ConnectionFactory() { HostName = "localhost" };
        _connection = _factory.CreateConnection();
        _channel = _connection.CreateModel();
        _mapper = new();
        
        _channel.QueueDeclare(queue:"myQueue", exclusive: false);

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (_, ea) =>
        {
            var messageString = Encoding.UTF8.GetString(ea.Body.ToArray());

            if (!_mapper.TryRemove(ea.BasicProperties.CorrelationId, out var taskComplete))
            {
                return;
            }

            if (taskComplete != null)
            {
                taskComplete.TrySetResult(messageString);
            }
            
            _channel.BasicAck(ea.DeliveryTag, false);

            
        };
        _channel.BasicConsume("myQueue", false, consumer);
    }

    public Task<object> SendAsync(string message, CancellationToken cancellationToken)
    {
        var props = _channel.CreateBasicProperties();
        var corId = Guid.NewGuid().ToString();

        props.CorrelationId = corId;
        props.ReplyTo = "myQueue";

        var messageByte = Encoding.UTF8.GetBytes(message);
        var taskComplete = new TaskCompletionSource<object>();

        _mapper.TryAdd(corId, taskComplete);
        
        _channel.BasicPublish(
            exchange: "",
            routingKey: "myQueue",
            basicProperties: props,
            body: messageByte
            );

        return taskComplete.Task;
    }
}