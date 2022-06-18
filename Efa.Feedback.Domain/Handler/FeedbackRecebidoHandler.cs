using Efa.Feedback.Domain.Request;
using Efa.Feedback.Domain.Response;
using MediatR;

namespace Efa.Feedback.Domain.Handler
{
    public class FeedbackRecebidoHandler : IRequestHandler<FeedbackRecebidoRequest, FeedbackRecebidoResponse>
    {
        private readonly IMediator _mediator;

        public FeedbackRecebidoHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        Task<FeedbackRecebidoResponse> IRequestHandler<FeedbackRecebidoRequest, FeedbackRecebidoResponse>.Handle(FeedbackRecebidoRequest request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"ID: {request.IdUser}");

            var response = new FeedbackRecebidoResponse() { Descricao = "teste" };

            return Task.FromResult(response);
        }
    }
}
