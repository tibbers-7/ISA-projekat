﻿using System;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using BloodBankAPI;
using BloodBankLibrary.Core.Appointments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebSocketsTutorial.Controllers
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
            var buffer = new byte[1024 * 4];
            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            //Wait until client initiates a request.
            _logger.Log(LogLevel.Information, "Message received from Client");

            //Going into a loop until the client closes the connection
            while (!result.CloseStatus.HasValue)
            {

                for(int i=1;i<=14;i++)
                {
                    Thread.Sleep(360);
                    string url = "https://localhost:44371/Location?id=" + i.ToString();
                    var loc = await client.GetStringAsync(url);

                    Location location = new Location(loc);
                    var serverMsg = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(location));
                    await webSocket.SendAsync(new ArraySegment<byte>(serverMsg, 0, serverMsg.Length), result.MessageType, result.EndOfMessage, CancellationToken.None);
                    _logger.Log(LogLevel.Information, "Message sent to Client");
                }
                //Wait until the client send another request.
                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    _logger.Log(LogLevel.Information, "Message received from Client");
                

            }
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
            _logger.Log(LogLevel.Information, "WebSocket connection closed");
        }
    }

    
}