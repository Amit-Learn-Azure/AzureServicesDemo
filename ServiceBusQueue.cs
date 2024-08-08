using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionAppPractice
{
    public class ServiceBusQueue
    {
        private readonly ILogger<ServiceBusQueue> _logger;

        public ServiceBusQueue(ILogger<ServiceBusQueue> logger)
        {
            _logger = logger;
        }

        [Function(nameof(ServiceBusQueue))]
        public async Task Run(
            [ServiceBusTrigger("messagequeue", Connection = "queueconnection")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
             var phone = message.ApplicationProperties["PhoneNo"].ToString();
            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message Body: {body}", message.Body);
            _logger.LogInformation("Phone no", phone);
            _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

            // Complete the message
            await messageActions.CompleteMessageAsync(message);

            // DeadLetter Queue
            //await messageActions.DeadLetterMessageAsync(message);
            
        }
    }
}
