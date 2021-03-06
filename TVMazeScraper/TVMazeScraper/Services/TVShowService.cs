using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TVMazeScraper.Models;
using TVMazeScraper.Models.Dtos;

namespace TVMazeScraper.Services
{
    public class TVShowService : ITVShowService
    {
        private readonly ScraperDbContext _context;
        private readonly int pageSize = 10;

        public TVShowService(ScraperDbContext context, ITVMazeService tVMazeService)
        {
            _context = context;
        }

        public async Task<IEnumerable<TVShow>> SearchTVShowWithCastAsync(string searchterm)
        {
            return _context.TVShows.Where(s => s.Name.Contains(searchterm)).ToList();
        }
    }
}
