using Efa.Feedback.Domain.Request;
using MediatR;

namespace Efa.Feedback.Domain.Handler
{
    public class ProcessarEnvioFeedbackRequestHandler : IRequestHandler<ProcessarEnvioFeedbackRequest, bool>
    {
        public Task<bool> Handle(ProcessarEnvioFeedbackRequest request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Email enviado!!");

            return Task.FromResult(true);
        }
    }
}
