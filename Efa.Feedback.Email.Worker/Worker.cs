using Efa.Feedback.Domain.Request;
using Efa.Feedback.RabbitMQ;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

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

            _messageHandler.IniciarConexao("queue.chegando","xch.chegando");

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();

        }

        await Task.Delay(1000, stoppingToken); //1segundo

        Console.WriteLine("O serviço está parando.");
    }

    public static void TratarMensagem(string messageRabbit)
    {
        if (string.IsNullOrEmpty(messageRabbit)) { return; }

        var message = JsonSerializer.Deserialize<EnviarFeedbackRequest>(messageRabbit);

        Console.WriteLine("Destino: " + message!.IdUserDestino);
    }


}