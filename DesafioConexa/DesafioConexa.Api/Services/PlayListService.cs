using DesafioConexa.Api.Models;
using DesafioConexa.Api.Interfaces;
using DesafioConexa.Api.Enums;

namespace DesafioConexa.Api.Services
{
    public class PlayListService : IPlayListService
    {
        private readonly IWeatherService _weatherService;
        private readonly IStreamingService _streamingService;

        public PlayListService(IWeatherService weatherService, IStreamingService streamingService)
        {
            _weatherService = weatherService;
            _streamingService = streamingService;
        }

        public PlayList GetPlayList(City city)
        {
            var temperatureInDegrees = _weatherService.GetTemperatureInDegrees(city);
            var playListCategory = GetPlayListCategory(temperatureInDegrees);
            var playlist = _streamingService.GetPlaylistRecommendation(playListCategory);

            return playlist;
        }

        public Category GetPlayListCategory(float temperature)
        {
            if (temperature > 30f)
            {
                return Category.PARTY;
            }

            if (temperature >= 15f && temperature <= 30f)
            {
                return Category.POP;
            }

            if (temperature >= 10f && temperature <= 14f)
            {
                return Category.ROCK;
            }

            return Category.CLASSICAL;
        }
    }
}
