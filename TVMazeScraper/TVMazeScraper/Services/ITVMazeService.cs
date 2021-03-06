using System.Threading.Tasks;
using TVMazeScraper.Models.Dtos;

namespace TVMazeScraper.Services
{
    public interface ITVMazeService
    {
        Task<TVShowDto> GetTVShowWithCastByIdAsync(long id);
    }
}
