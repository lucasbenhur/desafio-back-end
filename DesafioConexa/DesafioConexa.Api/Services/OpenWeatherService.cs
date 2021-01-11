using System;
using DesafioConexa.Api.Models;
using DesafioConexa.Api.Interfaces;
using Microsoft.Extensions.Options;
using RestSharp;
using Newtonsoft.Json.Linq;
using DesafioConexa.Api.Utils;

namespace DesafioConexa.Api.Services
{
    public class OpenWeatherService : IWeatherService
    {
        private const string Msg_Erro = "Erro ao obter a temperatura da cidade.";
        private readonly RestClient _restClient;

        public OpenWeatherService(IOptions<OpenWeatherSettings> openWeatherSettings)
        {
            _restClient = new RestClient(openWeatherSettings.Value.BaseUrl);
            _restClient.AddDefaultQueryParameter("appid", openWeatherSettings.Value.ApiKey);
            _restClient.AddDefaultQueryParameter("lang", "br");
            _restClient.AddDefaultQueryParameter("units", "metric");
        }

        public float GetTemperatureInDegrees(City city)
        {
            if (city == null)
            {
                throw new ArgumentNullException("Cidade não pode ser null");
            }

            if (!string.IsNullOrEmpty(city.Name))
            {
                return GetTemperatureInDegrees(city.Name);
            }

            return GetTemperatureInDegrees(city.Latitude, city.Longitude);
        }

        private float GetTemperatureInDegrees(string cityName)
        {
            var request = new RestRequest("/weather", Method.GET);
            request.AddQueryParameter("q", cityName);

            var response = _restClient.Execute<object>(request);

            if (response.IsSuccessful)
            {
                if (float.TryParse(JObject.Parse(response.Content)["main"]["temp"].ToString(), out float temperature))
                {
                    return temperature;
                }
            }

            throw new Exception(Msg_Erro);
        }

        private float GetTemperatureInDegrees(double? latitude, double? longitude)
        {
            var request = new RestRequest("/weather", Method.GET);
            request.AddQueryParameter("lat", latitude.ToString());
            request.AddQueryParameter("lon", longitude.ToString());

            var response = _restClient.Execute<object>(request);

            if (response.IsSuccessful)
            {
                if (float.TryParse(JObject.Parse(response.Content)["main"]["temp"].ToString(), out float temperature))
                {
                    return temperature;
                }
            }

            throw new Exception(Msg_Erro);
        }
    }
}
