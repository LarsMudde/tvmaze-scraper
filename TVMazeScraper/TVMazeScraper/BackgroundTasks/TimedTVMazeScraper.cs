using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Refit;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TVMazeScraper.Mappers;
using TVMazeScraper.Repositories;
using TVMazeScraper.Services;

namespace TVMazeScraper.BackgroundTasks
{
    public class TimedTVMazeScraper : IHostedService, IDisposable
    {
        private int executionCount = 0;
        private Timer _timer;
        private readonly ILogger<TimedTVMazeScraper> _logger;
        private readonly TVShowMapper _tVShowMapper;
        private readonly ActorMapper _actorMapper;
        private readonly IServiceScopeFactory _serviceProvider;

        public TimedTVMazeScraper(ILogger<TimedTVMazeScraper> logger, IServiceScopeFactory serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _tVShowMapper = new TVShowMapper();
            _actorMapper = new ActorMapper();
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Scraper running.");

            _timer = new Timer(Scrape, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(0.01));

            return Task.CompletedTask;
        }

        private async void Scrape(object state)
        {
            var count = Interlocked.Increment(ref executionCount);

            using (var scope = _serviceProvider.CreateScope())
            {
                var tVMazeService = scope.ServiceProvider.GetService<ITVMazeService>();
                var scraperRepository = scope.ServiceProvider.GetService<IScraperRepository>();
                try
                {
                    var scrapedShow = await tVMazeService.GetTVShowWithCastByIdAsync(count);
                    var tVShow = _tVShowMapper.fromDto(scrapedShow);
                    var cast = _actorMapper.fromDto(scrapedShow._embedded.Cast.ToList());

                    await scraperRepository.SaveOrUpdateTVShowWithCast(tVShow, cast);
                }
                catch (ApiException e)
                {
                    // Handle exceptions that don't justify a retry such as 404. (for now we just skip those)
                    _logger.LogError(
                    "Scraper error: {status}, message: {message}", e.StatusCode, e.Message);
                }
                catch (Exception e)
                {
                    _logger.LogError(
                    "Scraper error: {message}", e.Message);
                }
            }

            _logger.LogInformation(
                "Scraper is working. Count: {Count}", count);
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Scraper is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
