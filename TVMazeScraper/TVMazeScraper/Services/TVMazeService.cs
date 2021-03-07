using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Polly;
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
            var timeoutPolicy = Policy
            .Handle<ApiException>(ex => ex.StatusCode == HttpStatusCode.RequestTimeout)
            .RetryAsync(5, async (exception, retryCount) =>
            await Task.Delay(1000).ConfigureAwait(false));

            var fairUsePolicy = Policy
            .Handle<ApiException>(ex => ex.StatusCode == HttpStatusCode.TooManyRequests)
            .RetryAsync(5, async (exception, retryCount) =>
            await Task.Delay(1000).ConfigureAwait(false));

            return await timeoutPolicy.WrapAsync(fairUsePolicy)
                .ExecuteAsync(async () => await _tVMazeClient.GetTVShowWithCastById(id))
                .ConfigureAwait(false);
        }
    }
}
