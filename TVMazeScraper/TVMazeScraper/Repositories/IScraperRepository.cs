using System.Collections.Generic;
using System.Threading.Tasks;
using TVMazeScraper.Models;

namespace TVMazeScraper.Repositories
{
    interface IScraperRepository
    {
        Task SaveOrUpdateTVShowWithCast(TVShow tVShow, IEnumerable<Actor> cast);
    }
}
