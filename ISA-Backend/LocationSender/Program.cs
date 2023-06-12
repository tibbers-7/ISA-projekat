// See https://aka.ms/new-console-template for more information


using RabbitMQ.Client;

var factory = new ConnectionFactory()
{
    var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName ="user",
                Password = "password",
                VirtualHost ="/"
            };

var connection = factory.CreateConnection();

// create new channel if one doesnt alr exist
using var channel = connection.CreateModel();

//create queue
channel.QueueDeclare("bookings", durable: true, exclusive: true);

}
