using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TVMazeScraper.Models;

namespace TVMazeScraper.Repositories
{
    public interface IScraperRepository
    {
        Task<IEnumerable<TVShow>> SearchShowsWithCast(string searchTerm, uint page, uint pageSize, CancellationToken cancellationToken);
        Task SaveTVShowWithCast(TVShow tVShow, IEnumerable<Actor> cast);
    }
}
