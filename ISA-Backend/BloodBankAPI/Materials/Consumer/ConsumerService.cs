using BloodBankAPI.Model;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace BloodBankAPI.Materials.Consumer
{
    /*public void ConsumeMessages()
    {
        StoreLocation storage = StoreLocation.Instance;

        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            UserName = "user",
            Password = "password",
            VirtualHost = "/"
        };

        var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.QueueDeclare("locations", durable: true, exclusive: false, autoDelete: false);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, eventArgs) =>
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Location loc = JsonSerializer.Deserialize<Location>(message);
            storage.Store(loc);
        };

        channel.BasicConsume(queue: "locations", autoAck: true, consumer: consumer);
        Console.ReadKey();
    }
}
*/


    public class ConsumerService
    {
        private readonly IConfiguration _configuration;

        public ConsumerService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConsumeMessages()
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
                Console.ReadKey();
            }
        }
    }
}