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

        public async Task<TVShow> ScrapeTVShowByIdAsync(long id)
        {
            TVShow show = await _tVMazeService.GetTVShowByIdAsync(id);
            show.Cast = await _tVMazeService.GetCastByTVShowIdAsync(id);

            return show;
        }
    }
}
