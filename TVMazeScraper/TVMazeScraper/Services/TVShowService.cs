using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TVMazeScraper.Mappers;
using TVMazeScraper.Models.Dtos;
using TVMazeScraper.Repositories;

namespace TVMazeScraper.Services
{
    public class TVShowService : ITVShowService
    {
        private readonly IScraperRepository _scraperRepository;
        private TVShowMapper tVShowMapper;

        public TVShowService(IScraperRepository scraperRepository)
        {
            _scraperRepository = scraperRepository;
            tVShowMapper = new TVShowMapper();
        }

        /// <summary>
        /// Gets Shows with matching searchterm from repository and maps them to the right Data Transfer Object
        /// </summary>
        /// <param name="searchTerm">Word or part of word to search for</param>
        /// <param name="page">Which page to get</param>
        /// <param name="pageSize">How many results per page are desired</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>IEnumerable with TVShows and cast matching query</returns>
        public async Task<IEnumerable<TVShowResponseDto>> SearchTVShowWithCastAsync(string searchTerm, uint page, uint pagesize, CancellationToken cancellationToken)
        {
            return tVShowMapper.toResponseDto(await _scraperRepository.SearchShowsWithCast(searchTerm, page, pagesize, cancellationToken));
        }
    }
}
