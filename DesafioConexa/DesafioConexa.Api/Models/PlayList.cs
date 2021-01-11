using System.Collections.Generic;

namespace DesafioConexa.Api.Models
{
    public class PlayList
    {
        public PlayList()
        {
            TrackList = new List<Track>();
        }

        public List<Track> TrackList { get; set; }
    }
}
