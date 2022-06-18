using Efa.Feedback.Domain.Response;
using MediatR;

namespace Efa.Feedback.Domain.Request
{
    public class EnviarFeedbackRequest : IRequest<EnviarFeedbackResponse>
    {
        public int IdUserDestino { get; set; }

        public string? DescricaoPositiva { get; set; }

        public string? DescricaoNegativa { get; set; }

        public bool FeedbackAnonimo { get; set; }

    }
}
