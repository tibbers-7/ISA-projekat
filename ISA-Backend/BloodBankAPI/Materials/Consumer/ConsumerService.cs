using BloodBankAPI.Model;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace BloodBankAPI.Materials.Consumer
{


    public class ConsumerService : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider serviceProvider;

        IConnection connection;
        RabbitMQ.Client.IModel channel;

        public ConsumerService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task ConsumeMessages()
        {
            

        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            StoreLocation storage = StoreLocation.Instance;

            var factory = new ConnectionFactory { HostName = "localhost", Port = 5672, UserName = "guest", Password = "guest" };
            try
            {
                connection = factory.CreateConnection();
                channel = connection.CreateModel();
                channel.QueueDeclare(queue: "locations",
                                      durable: true,
                                      exclusive: false,
                                      autoDelete: false,
                                      arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (sender, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    // Handle the received message
                    Console.WriteLine("Received message: " + message);
                    Location loc = JsonSerializer.Deserialize<Location>(message);
                    storage.Store(loc);
                };

                channel.BasicConsume(queue: "locations",
                                       autoAck: true,
                                       consumer: consumer);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            };

            return Task.CompletedTask;
        }
    }
}