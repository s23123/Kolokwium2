using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IDbService _dbService;

        public AlbumController(IDbService dbService)
        {
            _dbService = dbService;
        }


        [HttpGet("albums/{id}")]
        public async Task<IActionResult> GetAlbums(int id)
        {
            try
            {
                var albums = await _dbService.GetAlbums(id);
                return Ok(albums);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("musican/{id}")]
        public async Task<IActionResult> DeleteMusican(int id)
        {
            try
            {
                await _dbService.DeleteMusican(id);
                return Ok($"Pomyślnie usunieto Muzyka o id {id}");
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
