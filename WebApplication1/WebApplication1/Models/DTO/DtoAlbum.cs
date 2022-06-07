using System;
using System.Collections.Generic;

namespace WebApplication1.Models.DTO
{
    public class DtoAlbum
    {
        public int IdAlbum { get; set; }
        public string AlbumName { get; set; }
        public DateTime PublishDate { get; set; }
        
        public virtual ICollection<DtoTrack> Track { get; set; }
    }
}
