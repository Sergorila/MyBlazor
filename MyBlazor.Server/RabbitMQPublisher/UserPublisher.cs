using System.Text;
using MyBlazor.Server.Views;
using RabbitMQ.Client;

namespace MyBlazor.Server.RabbitMQPublisher;

public static class UserPublisher
{
    static ConnectionFactory factory;
    static IConnection connection;
    static IModel channel;

    static UserPublisher()
    {
        factory = new ConnectionFactory() { HostName = "localhost" };
        connection = factory.CreateConnection();
        channel = connection.CreateModel();
    }
    
    public static void UserCreated(UserView user)
    {
        using (channel)
        {
            channel.QueueDeclare(
                queue: "myQueue",
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            var message = $"Был зарегистрирован пользователь : {DateTime.Now} {Environment.NewLine}" +
                          $"ФИО: {user.Fio}" +
                          $"Почта: {user.Mail}";
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