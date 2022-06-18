using MediatR;

namespace Efa.Feedback.Domain.Notification
{
    public record MessageNotification(string Message, string Exchange) : INotification;
}
