using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using TVMazeScraper.Clients;
using TVMazeScraper.Models;

namespace TVMazeScraper.Services
{
    public class TVMazeService : ITVMazeService
    {
        private readonly HttpClient _httpClient;
        private readonly ITVMazeClient _tVMazeClient;

        public TVMazeService()
        {
            _httpClient = new HttpClient(new HttpClientHandler()) { BaseAddress = new Uri("http://api.tvmaze.com") };
            _tVMazeClient = RestService.For<ITVMazeClient>(_httpClient);
        }

        public async Task<TVShow> GetTVShowByIdAsync(long id)
        {
            return await _tVMazeClient.GetTVShowById(id);
        }

        public async Task<IEnumerable<Actor>> GetCastByTVShowIdAsync(long id)
        {
            return await Task.FromResult(_tVMazeClient.GetCastByTVShowId(id).Result.Select(castMember => castMember.person).ToList());
        }
    }
}
