using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TVMazeScraper.Models.Dtos;

namespace TVMazeScraper.Services
{
    public interface ITVShowService
    {
        Task<IEnumerable<TVShowResponseDto>> SearchTVShowWithCastAsync(string searchTerm, uint page, uint pagesize, CancellationToken cancellationToken);
    }
}
