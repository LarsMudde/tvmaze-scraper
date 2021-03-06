using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
            _logger.LogInformation("Timed Hosted Service running.");

            _timer = new Timer(Scrape, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(1));

            return Task.CompletedTask;
        }

        private async void Scrape(object state)
        {
            var count = Interlocked.Increment(ref executionCount);

            using (var scope = _serviceProvider.CreateScope())
            {
                var tVMazeService = scope.ServiceProvider.GetService<ITVMazeService>();
                var scraperRepository = scope.ServiceProvider.GetService<IScraperRepository>();
                var scrapedShow = await tVMazeService.GetTVShowWithCastByIdAsync(count);
                var tVShow = _tVShowMapper.fromDto(scrapedShow);
                var cast = _actorMapper.fromDto(scrapedShow._embedded.Cast.ToList());

                await scraperRepository.SaveOrUpdateTVShowWithCast(tVShow, cast);
            }

            _logger.LogInformation(
                "Timed Hosted Service is working. Count: {Count}", count);
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
