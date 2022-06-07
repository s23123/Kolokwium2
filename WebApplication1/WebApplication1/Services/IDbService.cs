using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Models.DTO;

namespace WebApplication1.Services
{
    public interface IDbService
    {
        Task<IEnumerable<DtoAlbum>> GetAlbums(int id);
        Task DeleteMusican(int id);
    }
}
