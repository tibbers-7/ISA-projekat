﻿
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using BloodBankAPI.Model;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace BloodBankAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebSocketsController : ControllerBase
    {
        private readonly ILogger<WebSocketsController> _logger;
        private readonly HttpClient client;

        public WebSocketsController(ILogger<WebSocketsController> logger)
        {
            _logger = logger;
            client = new HttpClient();
        }

        //Add a new route called ws/
        [HttpGet("/ws")]
        public async Task Get()
        {
            //Check if the current request is via WebSockets otherwise throw a 400.
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                _logger.Log(LogLevel.Information, "WebSocket connection established");
                await Echo(webSocket);
            }
            else
            {
                HttpContext.Response.StatusCode = 400;
            }
        }


        private async Task Echo(WebSocket webSocket)
        {
            //singleton baza za trenutnu lokaciju
            StoreLocation storage = StoreLocation.Instance;
            var buffer = new byte[1024 * 4];
            // dobijanje poruke sa fronta
            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            
            //Wait until client initiates a request.
            _logger.Log(LogLevel.Information, "Message received from Client");

            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "user",
                Password = "password",
                VirtualHost = "/"
            };

            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare("locations", durable: true, exclusive: true);


            //Going into a loop until the client closes the connection
            while (!result.CloseStatus.HasValue)
            {
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, eventArgs) =>
                {
                    var body = eventArgs.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    var serverMsg = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
                    //await webSocket.SendAsync(new ArraySegment<byte>(serverMsg, 0, serverMsg.Length), result.MessageType, result.EndOfMessage, CancellationToken.None);
                    //_logger.Log(LogLevel.Information, "Message sent to Client");
                    storage.isNew = false;
                    _logger.Log(LogLevel.Information,message);
                };

                channel.BasicConsume(queue: "orders", autoAck: true, consumer: consumer);
                Console.ReadKey();
                //ako je nova posalji je na front
                
                        
                    
                                
                //Wait until the client send another request.
                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    _logger.Log(LogLevel.Information, "Message received from Client");
                

            }
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
            _logger.Log(LogLevel.Information, "WebSocket connection closed");
        }
    }




    
}