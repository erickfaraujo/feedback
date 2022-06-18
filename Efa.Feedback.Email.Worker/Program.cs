using Efa.Feedback.Domain.Handler;
using Efa.Feedback.Email.Worker;
using Efa.Feedback.RabbitMQ;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;

        services.AddHostedService<Worker>();

        services.AddSingleton<IConnectionFactory>(_ => new ConnectionFactory
        {
            Uri = new Uri(configuration.GetConnectionString("RabbitMQ"))
        });

        services.AddSingleton<IMessageHandler, MessageHandler>();

        services.AddMediatR(typeof(ProcessarEnvioFeedbackRequestHandler));

    })
    .Build();

await host.RunAsync();