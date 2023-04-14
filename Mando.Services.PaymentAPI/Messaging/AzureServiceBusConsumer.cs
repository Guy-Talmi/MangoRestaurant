using Azure.Messaging.ServiceBus;
using Mango.MessageBus;
using Mango.Services.PaymentAPI.Messages;
using Newtonsoft.Json;
using PaymentProcessor;
using System.Text;

namespace Mango.Services.PaymentAPI.Messaging
{
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    {
        private readonly string serviceBusConnectionString;
        private readonly string orderPaymentProcessTopic;
        private readonly string subscriptionPayment;
        private readonly string orderUpdatePaymentResultTopic;

        private readonly IProcessPayment _processPayment;
        private readonly IConfiguration _configuration;
        private readonly IMessageBus _messageBus;

        private ServiceBusProcessor orderPaymentProcessor;

        public AzureServiceBusConsumer(IConfiguration configuration, IMessageBus messageBus,
                                       IProcessPayment processPayment)
        {
            _configuration = configuration;
            _messageBus = messageBus;
            _processPayment = processPayment;

            serviceBusConnectionString = _configuration.GetValue<string>("ServiceBusConnectionString");
            orderPaymentProcessTopic = _configuration.GetValue<string>("OrderPaymentProcessTopic");
            subscriptionPayment = _configuration.GetValue<string>("OrderPaymentProcessSubscription");
            orderUpdatePaymentResultTopic = _configuration.GetValue<string>("OrderUpdatePaymentResultTopic");

            var client = new ServiceBusClient(serviceBusConnectionString);

            orderPaymentProcessor = client.CreateProcessor(orderPaymentProcessTopic, subscriptionPayment);
        }

        public async Task Start()
        {
            orderPaymentProcessor.ProcessMessageAsync += OnProcessPayment;
            orderPaymentProcessor.ProcessErrorAsync += OnErrorHandler;

            await orderPaymentProcessor.StartProcessingAsync();
        }

        public async Task Stop()
        {
            await orderPaymentProcessor.StopProcessingAsync();
            await orderPaymentProcessor.DisposeAsync();
        }

        private Task OnErrorHandler(ProcessErrorEventArgs arg)
        {
            Console.WriteLine(arg.Exception.ToString());
            return Task.CompletedTask;
        }

        private async Task OnProcessPayment(ProcessMessageEventArgs args)
        {
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);

            var paymentRequestMessage = JsonConvert.DeserializeObject<PaymentRequestMessage>(body);

            var result = _processPayment.PaymentProcessor();

            UpdatePaymentResultMessage updatePaymentResultMessage = new()
            {
                OrderId = paymentRequestMessage.OrderId,
                Status = result,
                Email = paymentRequestMessage.Email
            };

            try
            {
                await _messageBus.PublishMessage(updatePaymentResultMessage, orderUpdatePaymentResultTopic);
                await args.CompleteMessageAsync(message);
            }
            catch (Exception)
            {
                //Console.WriteLine("Error while publishing message to service bus.");
                throw;
            }
        }
    }
}
