using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TVMazeScraper.Models;

namespace TVMazeScraper.Repositories
{
    public interface IScraperRepository
    {
        Task<IEnumerable<TVShow>> SearchShowsWithCast(string query, uint page, uint pagesize, CancellationToken cancellationToken);
        Task SaveTVShowWithCast(TVShow tVShow, IEnumerable<Actor> cast);
    }
}
