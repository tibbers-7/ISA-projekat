using BloodBankAPI.Model;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace BloodBankAPI.Materials.Consumer
{


    public class ConsumerService 
    {
        private readonly IConfiguration _configuration;

        public ConsumerService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task ConsumeMessages()
        {
            StoreLocation storage = StoreLocation.Instance;

            var factory = new ConnectionFactory
            {
                Uri = new Uri(_configuration["RabbitMQ:ConnectionString"])
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: _configuration["RabbitMQ:QueueName"],
                    durable: true, 
                    exclusive: false, 
                    autoDelete: false
                );

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

                channel.BasicConsume(
                    queue: _configuration["RabbitMQ:QueueName"],
                    autoAck: true,
                    consumer: consumer
                );

                Console.WriteLine("Consumer started. Press any key to exit.");
                await Task.Run(() => Console.ReadKey());
            }

        }
    }
}