
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using BloodBankAPI.Materials.Consumer;
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
        private readonly StoreLocation storage;

        public WebSocketsController(ILogger<WebSocketsController> logger)
        {
            _logger = logger;
            client = new HttpClient();
            this.storage = StoreLocation.Instance;

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
            var buffer = new byte[1024 * 4];
            // dobijanje poruke sa fronta
            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            
            //Wait until client initiates a request.
            _logger.Log(LogLevel.Information, "Message received from Client");

            


            //Going into a loop until the client closes the connection
            while (!result.CloseStatus.HasValue)
            {

                if (!storage.IsEmpty())
                {
                    var serverMsg = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(storage.Retrieve()));
                    await webSocket.SendAsync(new ArraySegment<byte>(serverMsg, 0, serverMsg.Length), result.MessageType, result.EndOfMessage, CancellationToken.None);
                    _logger.Log(LogLevel.Information, "Message sent to Client");
                }            

                //Wait until the client send another request.
                //result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                  //_logger.Log(LogLevel.Information, "Message received from Client");
                

            }
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
            _logger.Log(LogLevel.Information, "WebSocket connection closed");
        }
    }




    
}