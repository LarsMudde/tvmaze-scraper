using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TVMazeScraper.Models;

namespace TVMazeScraper.Services
{
    public interface ITVShowService
    {
        Task<TVShow> GetTVShowByIdAsync(long id);
    }
}
