using Efa.Feedback.Domain.Notification;
using MediatR;
using RabbitMQ.Client;
using System.Text;

namespace Efa.Feedback.RabbitMQ
{
    public class MessageNotificationHandler : INotificationHandler<MessageNotification>
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageNotificationHandler(IConnectionFactory connectionFactory)
        {
            _connection = connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public Task Handle(MessageNotification notification, CancellationToken cancellationToken)
        {
            var (message, exchange) = notification;

            _channel.ExchangeDeclare(exchange, ExchangeType.Fanout, true, false, null);

            var properties = _channel.CreateBasicProperties();
            properties.ContentType = "application/json";

            _channel.BasicPublish(exchange,
                                  "",
                                  properties,
                                  Encoding.UTF8.GetBytes(message));

            return Task.CompletedTask;

        }
    }
}