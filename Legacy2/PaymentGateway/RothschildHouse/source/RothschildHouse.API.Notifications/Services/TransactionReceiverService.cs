using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RothschildHouse.API.Notifications.Hubs;
using RothschildHouse.Application.Queue;
using RothschildHouse.Application.Queue.Messages;

namespace RothschildHouse.API.Notifications.Services;

public class TransactionReceiverService : BackgroundService
{
    private readonly ILogger _logger;
    private readonly IHubContext<TransactionsHub> _hubContext;
    private readonly MqClientSettings _settings;
    private readonly ConnectionFactory _factory;
    private readonly IConnection _connection;
    private readonly IModel _model;
    private readonly IServiceProvider _serviceProvider;

    public TransactionReceiverService
    (
        ILogger<TransactionReceiverService> logger,
        IHubContext<TransactionsHub> hubContext,
        IOptions<MqClientSettings> options,
        IServiceProvider serviceProvider
    )
    {
        _logger = logger;
        _hubContext = hubContext;

        _settings = options.Value;

        _factory = new ConnectionFactory
        {
            HostName = _settings.HostName,
            Port = _settings.Port,
            UserName = _settings.UserName,
            Password = _settings.Password
        };

        _connection = _factory.CreateConnection();
        _model = _connection.CreateModel();

        _model.QueueDeclare(queue: _settings.Queue, durable: _settings.Durable, exclusive: _settings.Exclusive, autoDelete: _settings.AutoDelete, arguments: null);

        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_model);

        if (stoppingToken.IsCancellationRequested)
        {
            _logger?.LogInformation("Cancellation is been requested...");

            _model.Dispose();
            _connection.Dispose();

            await Task.CompletedTask;
        }

        consumer.Received += async (sender, args) =>
        {
            var request = PublishTransactionMessage.DeserializeFrom(args.Body.ToArray());

            _logger.LogInformation($"Received transaction: {request.ToJson()}");

            await _hubContext.Clients.All.SendAsync(HubMethods.ReceiveTxn, request.ClientApplication, request.Amount, request.Currency);
        };

        _model.BasicConsume(queue: _settings.Queue, autoAck: true, consumer: consumer);

        await Task.CompletedTask;
    }
}
