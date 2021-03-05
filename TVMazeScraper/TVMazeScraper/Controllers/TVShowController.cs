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

        [HttpGet("show/{id}")]
        public async Task<ActionResult<TVShow>> GetTVShowTest(long id)
        {
            var show = await _tVShowService.ScrapeTVShowByIdAsync(id);

            if (show == null)
            {
                return NotFound();
            }

            return show;
        }
    }
}
