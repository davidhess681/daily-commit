using KafkaTestConsumer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHostedService<ConsumerServiceA>();
builder.Services.AddHostedService<ConsumerServiceB>();

var app = builder.Build();

app.Run();
