﻿namespace WebApi.Controllers
{
    using System.Threading.Tasks;
    using Contracts;
    using MassTransit;
    using Microsoft.AspNetCore.Mvc;


    [ApiController]
    [Route("[controller]")]
    public class LocationController :
        ControllerBase
    {
        readonly IRequestClient<CheckLocation> _client;

        public LocationController(IRequestClient<CheckLocation> client)
        {
            _client = client;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            

            Response<LocationCoordinates> response = await _client.GetResponse<LocationCoordinates>(new { id });

            return Ok(response.Message);
        }
    }
}