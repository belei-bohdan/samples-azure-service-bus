using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;
using Shared.Configurations;
using System.Text.Json;

namespace QueueSenderAPI.Services
{
    public class SenderService : ISenderService
    {
        private readonly QueueOptions _options;

        public SenderService(IOptions<QueueOptions> options)
        {
            _options = options.Value;
        }

        public async Task SendMessageAsync<T>(T message)
        {
            await using ServiceBusClient client = new(_options.Connection);
            await using ServiceBusSender sender = client.CreateSender(_options.QueueName);

            var serviceBusMessage = ToServiceBusMessage(message);
            await sender.SendMessageAsync(serviceBusMessage);
        }

        private static ServiceBusMessage ToServiceBusMessage<T>(T message)
        {
            var messageBody = JsonSerializer.Serialize(message);
            return new ServiceBusMessage(messageBody);
        }
    }
}
