using Efa.Feedback.Domain.Request;
using MediatR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Efa.Feedback.RabbitMQ
{
    public class MessageHandler : IMessageHandler
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IMediator _mediator;

        public MessageHandler(IConnectionFactory connectionFactory, IMediator mediator)
        {
            _connection = connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
            _mediator = mediator;
        }

        public void IniciarConexao(string nomeFila, string nomeExchange)
        {
            CriaExchangeFila(nomeFila, nomeExchange);

            var consumer = new EventingBasicConsumer(_channel);
            var message = "";

            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                message = Encoding.UTF8.GetString(body);

                Console.WriteLine("Iniciando processamento da mensagem");

                await ProcessarMensagem(message);

                Console.WriteLine("Mensagem processada com sucesso!");
                Console.WriteLine("");
            };

            _channel.BasicConsume(queue: nomeFila,
                                 autoAck: true,
                                 consumer: consumer);
        }

        private Task ProcessarMensagem(string message)
        {
            var mensagem = JsonSerializer.Deserialize<ProcessarEnvioFeedbackRequest>(message);

            _ = _mediator.Send(mensagem!);

            return Task.CompletedTask;
        }

        private void CriaExchangeFila(string nomeFila, string nomeExchange)
        {
            _channel.ExchangeDeclare(nomeExchange,
                                     ExchangeType.Fanout,
                                     true,
                                     false,
                                     null);

            _channel.QueueDeclare(queue: nomeFila);

            _channel.QueueBind(queue: nomeFila,
                               exchange: nomeExchange,
                               routingKey: "");
        }
    }
}
