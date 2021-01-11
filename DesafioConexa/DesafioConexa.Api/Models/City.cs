namespace DesafioConexa.Api.Models
{
    public class City
    {
        public City(string cityName, double? latitude, double? longitude)
        {
            Name = cityName;
            Latitude = latitude;
            Longitude = longitude;
        }

        public string Name { get; private set; }
        public double? Latitude { get; private set; }
        public double? Longitude { get; private set; }
    }
}
