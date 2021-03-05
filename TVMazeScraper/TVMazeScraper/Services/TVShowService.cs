using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TVMazeScraper.Models;

namespace TVMazeScraper.Services
{
    public class TVShowService : ITVShowService
    {
        private readonly TVShowContext _context;
        private readonly ITVMazeService _tVMazeService;

        public TVShowService(TVShowContext context, ITVMazeService tVMazeService)
        {
            _context = context;
            _tVMazeService = tVMazeService;
        }

        public async Task<TVShow> GetTVShowByIdAsync(long id)
        {
            var show = _context.TVShows.Find(id);

            if (show == null)
            {
                return await ScrapeAndPersistTVShowByIdAsync(id);
            }

            return show;
        }

        private async Task<TVShow> ScrapeAndPersistTVShowByIdAsync(long id)
        {
            TVShow show = await _tVMazeService.GetTVShowByIdAsync(id);
            show.Cast = await _tVMazeService.GetCastByTVShowIdAsync(id);
            _context.Add(show);
            return show;
        }
    }
}
