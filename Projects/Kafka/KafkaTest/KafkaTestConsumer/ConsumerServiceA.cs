using Confluent.Kafka;

namespace KafkaTestConsumer;

public class ConsumerServiceA : BackgroundService
{
    private readonly IConsumer<Ignore, string> _consumer;

    private readonly ILogger<ConsumerServiceA> _logger;

    public ConsumerServiceA(IConfiguration configuration, ILogger<ConsumerServiceA> logger)
    {
        _logger = logger;

        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = configuration["Kafka:BootstrapServers"],
            GroupId = "InventoryConsumerGroupA",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        _consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _consumer.Subscribe("InventoryUpdates");

        while (!stoppingToken.IsCancellationRequested)
        {
            ProcessKafkaMessage(stoppingToken);

            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }

        _consumer.Close();
    }

    public void ProcessKafkaMessage(CancellationToken stoppingToken)
    {
        try
        {
            var consumeResult = _consumer.Consume(stoppingToken);
            
            var message = consumeResult.Message.Value;

            _logger.LogInformation($"Consumer A: Received inventory update: {message}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error processing Kafka message: {ex.Message}");
        }
    }
}
