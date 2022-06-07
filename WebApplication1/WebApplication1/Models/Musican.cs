using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class Musican
    {
        public int IdMusican { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }

        public virtual ICollection<Musican_Track> Musican_Track { get; set; }
    }
}
