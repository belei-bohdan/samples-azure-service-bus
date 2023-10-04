using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Shared.Configurations;
using Shared.Models;
using System.Text.Json;

internal class QueueProcessor : BackgroundService
{
    private readonly QueueOptions _options;

    public QueueProcessor(IOptions<QueueOptions> options)
    {
        _options = options.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await using var client = new ServiceBusClient(_options.Connection);

        var options = new ServiceBusProcessorOptions
        {
            AutoCompleteMessages = false,
            MaxConcurrentCalls = 1,
        };

        await using var processor = client.CreateProcessor(_options.QueueName);

        processor.ProcessMessageAsync += MessageHandler;
        processor.ProcessErrorAsync += ErrorHandler;

        await processor.StartProcessingAsync(stoppingToken);

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(3000, stoppingToken);
                Console.WriteLine("Listening...");
            }
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Listening cancelled.");
            await processor.CloseAsync(stoppingToken);
        }
    }

    async Task MessageHandler(ProcessMessageEventArgs args)
    {
        string body = args.Message.Body.ToString();
        var message = JsonSerializer.Deserialize<Message>(body);

        if (message is not null)
        {
            DisplayMessage(message);
            await args.CompleteMessageAsync(args.Message);
        }
    }

    private static void DisplayMessage(Message message)
    {
        Console.WriteLine($"[{DateTime.Now.TimeOfDay:hh\\:mm\\:ss}] {message.Author}: {message.Content}");
    }

    Task ErrorHandler(ProcessErrorEventArgs args)
    {
        Console.WriteLine(args.ErrorSource);
        Console.WriteLine(args.FullyQualifiedNamespace);
        Console.WriteLine(args.EntityPath);
        Console.WriteLine(args.Exception.Message);

        return Task.CompletedTask;
    }
}