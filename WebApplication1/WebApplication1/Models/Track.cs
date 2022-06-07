using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class Track
    {
        public int IdTrack { get; set; }
        public string TrackName { get; set; }
        public float Duration { get; set; }
        public int IdMusicAlbum { get; set; }

        public virtual ICollection<Musican_Track> Musican_Track { get; set; }
        public virtual Album Album { get; set; }
    }
}
