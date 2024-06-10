using System.Text;
using MyBlazor.Server.Views;
using RabbitMQ.Client;

namespace MyBlazor.Server.RabbitMQPublisher;

public static class CategoryPublisher
{
    static ConnectionFactory factory;
    static IConnection connection;
    static IModel channel;

    static CategoryPublisher()
    {
        factory = new ConnectionFactory() { HostName = "localhost" };
        connection = factory.CreateConnection();
        channel = connection.CreateModel();
    }
    
    public static void CategoryCreated(CategoryView category)
    {
        using (channel)
        {
            channel.QueueDeclare(
                queue: "myQueue",
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            var message = $"Была добавлена категория : {DateTime.Now} {Environment.NewLine}" +
                          $"Название: {category.Title}";
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(
                exchange: "",
                routingKey: "myQueue",
                basicProperties: null,
                body: body
            );
        }
    }
}