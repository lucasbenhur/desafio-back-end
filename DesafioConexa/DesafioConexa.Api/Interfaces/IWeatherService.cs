using DesafioConexa.Api.Models;

namespace DesafioConexa.Api.Interfaces
{
    public interface IWeatherService
    {
        float GetTemperatureInDegrees(City city);
    }
}
