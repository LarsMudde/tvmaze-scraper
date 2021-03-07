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

        /// <summary>
        /// Searches for searchterm in the name of a tv show and returns a paged result
        /// </summary>
        /// <param name="searchTerm">Word or part of word to search for</param>
        /// <param name="page">Which page to get</param>
        /// <param name="pageSize">How many results per page are desired</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>IEnumerable with TVShows and cast matching searchTerm</returns>
        [HttpGet("/search/{searchTerm}")]
        public async Task<ActionResult<IEnumerable<TVShowResponseDto>>> SearchTVShowPaged(string searchTerm, [FromQuery] uint page = 0, [FromQuery] uint pageSize = 10, CancellationToken cancellation = default(CancellationToken))
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
