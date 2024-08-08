using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionAppPractice
{
    public class ServiceBusTopic
    {
        private readonly ILogger<ServiceBusTopic> _logger;

        public ServiceBusTopic(ILogger<ServiceBusTopic> logger)
        {
            _logger = logger;
        }

        [Function(nameof(ServiceBusTopic))]
        public async Task Run(
            [ServiceBusTrigger("firsttopic", "S1", Connection = "TopicConnection")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message Body: {body}", message.Body);
            _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

             // Complete the message
            await messageActions.CompleteMessageAsync(message);
        }
    }
}
