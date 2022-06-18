using Efa.Feedback.Domain.Notification;
using Efa.Feedback.Domain.Request;
using Efa.Feedback.Domain.Response;
using MediatR;
using System.Text.Json;

namespace Efa.Feedback.Domain.Handler
{
    public class EnviarFeedbackRequestHandler : IRequestHandler<EnviarFeedbackRequest, EnviarFeedbackResponse>
    {
        private readonly IMediator _mediator;
        private readonly string _exchange = "xch.chegando";

        public EnviarFeedbackRequestHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<EnviarFeedbackResponse> Handle(EnviarFeedbackRequest request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Inserindo Feedback na fila de Envio");

            var message = JsonSerializer.Serialize(request);

            Console.WriteLine($"JSON: {message}");

            await _mediator.Publish(new MessageNotification(message, _exchange));

            var response = new EnviarFeedbackResponse() { Descricao = "teste" };

            return await Task.FromResult(response);
        }
    }
}
