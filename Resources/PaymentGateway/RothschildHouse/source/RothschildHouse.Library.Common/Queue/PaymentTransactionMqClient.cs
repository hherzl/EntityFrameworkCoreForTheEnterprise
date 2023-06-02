using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RothschildHouse.Library.Common.Queue.Messages;

namespace RothschildHouse.Library.Common.Queue
{
    public class PaymentTransactionMqClient
    {
        private readonly ILogger _logger;

        public PaymentTransactionMqClient(ILogger<PaymentTransactionMqClient> logger, IOptions<MqClientSettings> options)
        {
            _logger = logger;
            Settings = options.Value;
        }

        public MqClientSettings Settings { get; }

        public PublishPaymentTransactionResult Publish(PublishPaymentTransactionMessage request)
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = Settings.HostName,
                Port = Settings.Port,
                UserName = Settings.UserName,
                Password = Settings.Password
            };

            _logger?.LogInformation($"Creating connection as '{Settings.UserName}' to Rabbit MQ instance : '{Settings.HostName}:{Settings.Port}'");

            using var connection = connectionFactory.CreateConnection();

            using var model = connection.CreateModel();

            var result = new PublishPaymentTransactionResult();

            try
            {
                model.QueueDeclare(queue: Settings.Queue, autoDelete: Settings.AutoDelete, exclusive: Settings.Exclusive, arguments: null);

                _logger?.LogInformation($"Publishing {request.ToJson()} in message queue: {Settings.Queue}");

                result.Id = string.Format("{0}-{1}", DateTime.Now.ToString("yyyyMMdd"), request.Id);

                model.BasicPublish(exchange: "", body: request.ToBytes(), routingKey: Settings.Queue);

                result.Message = $"The message publishing in queue: '{Settings.Queue}' for payment transaction '{request.Id}' was successfully, Id: '{result.Id}'";

                _logger?.LogInformation(result.Message);
            }
            catch (Exception ex)
            {
                _logger?.LogCritical(ex, $"There was an error publishing queue message in: '{Settings.Queue}' for payment transaction: '{request.Id}'");

                result.Failed = true;
            }

            return result;
        }
    }
}
