using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using TVMazeScraper.Models;

namespace TVMazeScraper.Clients
{
    public interface ITVMazeClient
    {
        [Get("/shows/{id}")]
        Task<TVShow> GetTVShowById(long id);

        [Get("/shows/{id}/cast")]
        Task<IEnumerable<CastDto>> GetCastByTVShowId(long id);
    }
}
