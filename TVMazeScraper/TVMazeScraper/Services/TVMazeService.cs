using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HttpClientDiagnostics;
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
            _httpClient = new HttpClient(new HttpClientDiagnosticsHandler(new HttpClientHandler())) { BaseAddress = new Uri("http://api.tvmaze.com") };
            _tVMazeClient = RestService.For<ITVMazeClient>(_httpClient);
        }

        public async Task<ActorDto> GetActorByIdAsync(long id)
        {
            return await _tVMazeClient.GetActorById(id);
        }
    }
}
