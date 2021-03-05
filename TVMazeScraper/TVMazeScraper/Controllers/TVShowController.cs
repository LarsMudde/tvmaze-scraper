using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<TVShow>> GetTVShow(long id)
        {
            var show = await _tVShowService.GetTVShowByIdAsync(id);

            if (show == null)
            {
                return NotFound();
            }

            return show;
        }
    }
}
