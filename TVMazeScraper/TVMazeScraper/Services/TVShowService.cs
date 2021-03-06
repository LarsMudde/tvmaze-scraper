using AutoMapper;
using System.Threading.Tasks;
using TVMazeScraper.Models;
using TVMazeScraper.Models.Dtos;

namespace TVMazeScraper.Services
{
    public class TVShowService : ITVShowService
    {
        private readonly ScraperDbContext _context;

        public TVShowService(ScraperDbContext context, ITVMazeService tVMazeService)
        {
            _context = context;
        }

        public async Task<TVShow> GetTVShowWithCastByIdAsync(long id)
        {
            return _context.TVShows.Find(id);
        }
    }
}
