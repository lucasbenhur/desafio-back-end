using System;
using DesafioConexa.Api.Models;
using DesafioConexa.Api.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DesafioConexa.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayListController : ControllerBase
    {
        private readonly ILogger<PlayListController> _logger;
        private readonly IPlayListService _playListService;

        public PlayListController(ILogger<PlayListController> logger, IPlayListService playListService)
        {
            _logger = logger;
            _playListService = playListService;
        }

        [HttpGet]
        public ActionResult<PlayList> Get(
            [FromQuery(Name = "city")] string cityName, 
            [FromQuery(Name = "lat")] double? latitude, 
            [FromQuery(Name = "lon")] double? longitude)
        {
            City city = null;

            if (!string.IsNullOrEmpty(cityName))
            {
                city = new City(cityName, null, null);
            }
            else if (latitude.HasValue && longitude.HasValue)
            {
                city = new City(string.Empty, latitude, longitude);
            }

            if (city == null)
            {
                return BadRequest();
            }

            try
            {
                var playList = _playListService.GetPlayList(city);
                return playList;
            }
            catch (Exception exception)
            {
                _logger.LogError("Error: PlayListController", exception);
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno do servidor.");
            }
        }
    }
}
