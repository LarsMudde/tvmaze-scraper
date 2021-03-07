using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Polly;
using Refit;
using TVMazeScraper.Clients;
using TVMazeScraper.Models.Dtos;

namespace TVMazeScraper.Services
{
    public class TVMazeService : ITVMazeService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly ITVMazeClient _tVMazeClient;

        public TVMazeService(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new(new HttpClientHandler()) { BaseAddress = new Uri(_configuration["TVMazeScraperSettings:BaseUrl"]) };
            _tVMazeClient = RestService.For<ITVMazeClient>(_httpClient);
        }

        /// <summary>
        /// Queries the TVMaze API with the id of a show to get the show and cast.
        /// </summary>
        /// <param name="id">The Id of the show to query the TVMaze API for</param>
        /// <returns>TVShowDTO, a Dto containing the TVShow and cast. The model can be extended to store more of the data</returns>
        public async Task<TVShowDto> GetTVShowWithCastByIdAsync(long id)
        {
            var timeoutPolicy = Policy
            .Handle<ApiException>(ex => ex.StatusCode == HttpStatusCode.RequestTimeout)
            .RetryAsync(_configuration.GetValue<int>("TVMazeScraperSettings:RetriesOnFailure"), async (exception, retryCount) =>
            await Task.Delay(_configuration.GetValue<int>("TVMazeScraperSettings:RetryDelay")).ConfigureAwait(false));

            var fairUsePolicy = Policy
            .Handle<ApiException>(ex => ex.StatusCode == HttpStatusCode.TooManyRequests)
            .RetryAsync(_configuration.GetValue<int>("TVMazeScraperSettings:RetriesOnFailure"), async (exception, retryCount) =>
            await Task.Delay(_configuration.GetValue<int>("TVMazeScraperSettings:RetryDelay")).ConfigureAwait(false));

            return await timeoutPolicy.WrapAsync(fairUsePolicy)
                .ExecuteAsync(async () => await _tVMazeClient.GetTVShowWithCastById(id))
                .ConfigureAwait(false);
        }
    }
}
