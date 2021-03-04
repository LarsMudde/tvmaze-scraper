using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TVMazeScraper.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TVMazeScraper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TVShowController : ControllerBase
    {

        private readonly TVShowContext _context;

        public TVShowController(TVShowContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostTest(TVShow tvShow)
        {
            _context.TVShows.Add(tvShow);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TVShow>> GetTest(long id)
        {
            var tvShow = await _context.TVShows.FindAsync(id);

            if (tvShow == null)
            {
                return NotFound();
            }

            return tvShow;
        }
    }
}
