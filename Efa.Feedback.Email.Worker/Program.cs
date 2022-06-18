using Efa.Feedback.Email.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using Efa.Feedback.RabbitMQ;
using MediatR;
using Efa.Feedback.Domain.Handler;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();

        services.AddSingleton<IConnectionFactory>(_ => new ConnectionFactory
        {
            Uri = new Uri("amqp://guest:guest@localhost:5672")
        });

        services.AddSingleton<IMessageHandler, MessageHandler>();

        services.AddMediatR(typeof(ProcessarEnvioFeedbackRequestHandler));

    })
    .Build();

await host.RunAsync();