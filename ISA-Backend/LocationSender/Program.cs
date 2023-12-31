﻿// See https://aka.ms/new-console-template for more information

using LocationSender;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName ="guest",
                Password = "guest",
                VirtualHost ="/"
            };

var connection = factory.CreateConnection();

// create new channel if one doesnt alr exist
var channel = connection.CreateModel();

//create queue
channel.QueueDeclare("locations", durable: true, exclusive: false, autoDelete: false) ;

IDictionary<int, Location> locations;

locations = new Dictionary<int, Location>();
IEnumerable<string> lines = File.ReadLines(Directory.GetCurrentDirectory() + "/locations.txt");


foreach (string line in lines)
{
    string[] data = line.Split(",");
    Location location = new Location { Id = int.Parse(data[0]), Latitude = float.Parse(data[1]), Longitude = float.Parse(data[1]) };
    locations.Add(int.Parse(data[0]), location);

    var jsonString = JsonSerializer.Serialize(location);
    var body = Encoding.UTF8.GetBytes(jsonString);
    channel.BasicPublish("", "locations", body: body);
    Console.WriteLine("Published " + location);

    Thread.Sleep(1000);
}



