using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TVMazeScraper.Models;
using TVMazeScraper.Services;

namespace TVMazeScraper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TVShowController : ControllerBase
    {

        private readonly TVShowContext _context;
        private readonly ITVMazeService _tVMazeService;

        public TVShowController(TVShowContext context, ITVMazeService tVMazeService)
        {
            _context = context;
            _tVMazeService = tVMazeService;
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

        [HttpGet("actor/{id}")]
        public async Task<ActionResult<ActorDto>> GetActorTest(long id)
        {
            var actor = await _tVMazeService.GetActorByIdAsync(id);

            if (actor == null)
            {
                return NotFound();
            }

            return actor;
        }
    }
}
