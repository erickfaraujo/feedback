using Efa.Feedback.RabbitMQ;
using Microsoft.Extensions.Hosting;

namespace Efa.Feedback.Email.Worker;

public class Worker : BackgroundService
{
    private readonly IMessageHandler _messageHandler;

    public Worker(IMessageHandler messageHandler)
    {
        _messageHandler = messageHandler;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("O serviço está iniciando.");

        while (!stoppingToken.IsCancellationRequested)
        {
            Console.WriteLine($"Rodando: {DateTime.Now}");

            _messageHandler.IniciarConexao("queue.chegando", "xch.chegando");

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();

        }

        await Task.Delay(1000, stoppingToken); //1segundo

        Console.WriteLine("O serviço está parando.");
    }

}