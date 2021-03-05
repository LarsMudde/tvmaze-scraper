using System.Collections.Generic;
using System.Threading.Tasks;
using TVMazeScraper.Models;

namespace TVMazeScraper.Services
{
    public interface ITVMazeService
    {
        Task<TVShow> GetTVShowByIdAsync(long id);
        Task<IEnumerable<Actor>> GetCastByTVShowIdAsync(long id);
    }
}
