using System;
using System.Net.Http;
using System.Threading.Tasks;
using Refit;
using TVMazeScraper.Clients;
using TVMazeScraper.Models.Dtos;

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

        public async Task<TVShowDto> GetTVShowWithCastByIdAsync(long id)
        {
            return await _tVMazeClient.GetTVShowWithCastById(id);
        }
    }
}
