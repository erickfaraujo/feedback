using Efa.Feedback.Domain.Request;
using Efa.Feedback.RabbitMQ;
using MediatR;
using RabbitMQ.Client;

namespace Efa.Feedback.Api.Configure
{
    public static class ConfigureDependencies
    {

        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            AddMediatR(services);
            AddRabbitMQ(services, configuration);
        }

        private static void AddRabbitMQ(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConnectionFactory>(_ => new ConnectionFactory
            {
                Uri = new Uri(configuration["RabbitMQ:ConnectionString"])
            });
        }

        public static void AddMediatR(IServiceCollection services)
        {
            services.AddMediatR(typeof(FeedbackRecebidoRequest));
            services.AddMediatR(typeof(EnviarFeedbackRequest));
            services.AddMediatR(typeof(MessageNotificationHandler));
        }
    }
}
