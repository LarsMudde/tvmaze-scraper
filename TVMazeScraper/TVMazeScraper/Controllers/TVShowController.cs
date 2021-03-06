using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TVMazeScraper.Models.Dtos;
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

        [HttpGet("/search/{searchTerm}")]
        public async Task<ActionResult<IEnumerable<TVShowResponseDto>>> SearchTVShowPaged(string searchTerm, [FromQuery] int page = 0, [FromQuery] int pageSize = 10, CancellationToken cancellation = default(CancellationToken))
        {
            var show = await _tVShowService.SearchTVShowWithCastAsync(searchTerm, page, pageSize, cancellation);

            if (show == null)
            {
                return NotFound();
            }

            return Ok(show);
        }
    }
}
