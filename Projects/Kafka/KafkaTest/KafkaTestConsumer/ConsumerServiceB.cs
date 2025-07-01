using Confluent.Kafka;

namespace KafkaTestConsumer;

public class ConsumerServiceB : BackgroundService
{
    private readonly IConsumer<Ignore, string> _consumer;

    private readonly ILogger<ConsumerServiceB> _logger;

    public ConsumerServiceB(IConfiguration configuration, ILogger<ConsumerServiceB> logger)
    {
        _logger = logger;

        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = configuration["Kafka:BootstrapServers"],
            GroupId = "InventoryConsumerGroupB",
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

            _logger.LogInformation($"Consumer B: Received inventory update: {message}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error processing Kafka message: {ex.Message}");
        }
    }
}
