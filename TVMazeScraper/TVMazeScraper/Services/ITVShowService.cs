using System.Threading.Tasks;
using TVMazeScraper.Models;

namespace TVMazeScraper.Services
{
    public interface ITVShowService
    {
        Task<TVShow> GetTVShowWithCastByIdAsync(long id);
    }
}
