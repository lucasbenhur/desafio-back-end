namespace DesafioConexa.Api.Models
{
    public class Track
    {
        public Track(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
