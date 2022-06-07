using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Models.DTO;

namespace WebApplication1.Services
{
    public class DbService : IDbService
    {
        private readonly MainDbContext _mainDbContext;

        public DbService(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public async Task DeleteMusican(int id)
        {
            using (var transaction = await _mainDbContext.Database.BeginTransactionAsync()) {
                var musicanExists = await _mainDbContext.Musicans.AnyAsync(e => e.IdMusican == id);
                var musicanHasTracks = await _mainDbContext.Musican_Tracks.AnyAsync(e => e.IdMusican == id);
                var tracksHasAlbums = await _mainDbContext.Tracks.AnyAsync(e => e.IdMusicAlbum.Equals(null));
                if (!musicanExists)
                {
                    throw new System.Exception($"Nie ma w bazie danych muzyka o id {id}");
                }
                if (musicanHasTracks && !tracksHasAlbums)
                {
                    var remove = new Musican() { IdMusican = id };
                    _mainDbContext.Attach(remove);
                    _mainDbContext.Remove(remove);
                    _mainDbContext.SaveChangesAsync();
                    transaction.Commit();

                }
                else
                {
                    throw new System.Exception($"Nie mozna usunac muzyka o id {id} poniewaz nie ma piosenek lub sa przypisane do albumu");
                    transaction.Rollback();
                }
            }
        }

        public async Task<IEnumerable<DtoAlbum>> GetAlbums(int id)
        {
            var albumExists = await _mainDbContext.Albums.AnyAsync(e => e.IdAlbum == id);
            if (!albumExists)
            {
                throw new System.Exception($"W bazie nie ma albumu o id {id}");
            }
            var resp = await _mainDbContext.Albums
                .Include(e => e.Tracks)
                .Where(e => e.IdAlbum == id)
                .Select(e => new DtoAlbum
                {
                    IdAlbum = e.IdAlbum,
                    AlbumName = e.AlbumName,
                    PublishDate = e.PublishDate,

                    Track = e.Tracks
                    .OrderBy(e => e.Duration)
                    .Select(e => new DtoTrack
                    {
                        IdTrack = e.IdTrack,
                        TrackName = e.TrackName,
                        Duration = e.Duration
                    }).ToList()
                }).ToListAsync();

           

            return resp;                
        }
    }
}
