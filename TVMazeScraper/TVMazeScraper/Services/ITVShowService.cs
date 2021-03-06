using System.Collections.Generic;
using System.Threading.Tasks;
using TVMazeScraper.Models;

namespace TVMazeScraper.Services
{
    public interface ITVShowService
    {
        Task<IEnumerable<TVShow>> SearchTVShowWithCastAsync(string searchterm);
    }
}
