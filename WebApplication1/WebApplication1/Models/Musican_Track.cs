namespace WebApplication1.Models
{
    public class Musican_Track
    {
        public int IdTrack { get; set; }
        public int IdMusican { get; set; }
        public virtual Musican Musican { get; set; }
        public virtual Track Track { get; set; }
    }
}
