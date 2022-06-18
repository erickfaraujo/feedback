using Efa.Feedback.Domain.Response;
using MediatR;

namespace Efa.Feedback.Domain.Request
{
    public class FeedbackRecebidoRequest : IRequest<FeedbackRecebidoResponse>
    {
        public int IdUser { get; set; }

    }
}
