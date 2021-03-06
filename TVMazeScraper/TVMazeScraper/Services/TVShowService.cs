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

        public async Task<IEnumerable<TVShowResponseDto>> SearchTVShowWithCastAsync(string searchterm, int page, int pagesize, CancellationToken cancellationToken)
        {
            return tVShowMapper.toResponseDto(await _scraperRepository.SearchShowsWithCast(searchterm, page, pagesize, cancellationToken));
        }
    }
}
