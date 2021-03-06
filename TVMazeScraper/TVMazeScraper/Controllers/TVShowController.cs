using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TVMazeScraper.Models;
using TVMazeScraper.Services;

namespace TVMazeScraper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TVShowController : ControllerBase
    {
        private readonly ITVShowService _tVShowService;

        public TVShowController(ITVShowService tVShowService)
        {
            _tVShowService = tVShowService;
        }

        [HttpGet("{searchTerm}")]
        public async Task<ActionResult<IEnumerable<TVShow>>> GetTVShow(string searchTerm)
        {
            var show = await _tVShowService.SearchTVShowWithCastAsync(searchTerm);

            if (show == null)
            {
                return NotFound();
            }

            return Ok(show);
        }
    }
}
