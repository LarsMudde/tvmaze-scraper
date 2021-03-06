using System.Threading.Tasks;
using Refit;
using TVMazeScraper.Models.Dtos;

namespace TVMazeScraper.Clients
{
    public interface ITVMazeClient
    {
        [Get("/shows/{id}?embed=cast")]
        Task<TVShowDto> GetTVShowWithCastById(long id);
    }
}
