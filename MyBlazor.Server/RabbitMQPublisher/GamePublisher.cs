using System.Text;
using MyBlazor.Server.Views;
using RabbitMQ.Client;

namespace MyBlazor.Server.RabbitMQPublisher;

public static class GamePublisher
{
    static ConnectionFactory factory;
    static IConnection connection;
    static IModel channel;

    static GamePublisher()
    {
        factory = new ConnectionFactory() { HostName = "localhost" };
        connection = factory.CreateConnection();
        channel = connection.CreateModel();
    }
    
    public static void GameCreated(GameView game)
    {
        using (channel)
        {
            channel.QueueDeclare(
                queue: "myQueue",
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            var message = $"Была добавлена игра : {DateTime.Now} {Environment.NewLine}" +
                          $"Название: {game.Title}" +
                          $"Цена: {game.Cost}";
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