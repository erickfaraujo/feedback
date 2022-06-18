using MediatR;

namespace Efa.Feedback.RabbitMQ
{
    public interface IMessageHandler
    {
        void IniciarConexao(string nomeFila, string nomeExchange);

    }
}
