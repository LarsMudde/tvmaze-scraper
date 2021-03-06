using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TVMazeScraper.Mappers;
using TVMazeScraper.Models;
using TVMazeScraper.Services;

namespace TVMazeScraper.BackgroundTasks
{
    public class TimedTVMazeScraper : IHostedService, IDisposable
    {
        private int executionCount = 0;
        private Timer _timer;
        private readonly ILogger<TimedTVMazeScraper> _logger;
        private readonly TVShowMapper _tVShowMapper;
        private readonly IServiceScopeFactory _serviceProvider;

        public TimedTVMazeScraper(ILogger<TimedTVMazeScraper> logger, IServiceScopeFactory serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _tVShowMapper = new TVShowMapper();
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
                var scraperDbContext = scope.ServiceProvider.GetService<ScraperDbContext>();
                var tVShow = _tVShowMapper.fromDto(await tVMazeService.GetTVShowWithCastByIdAsync(count));

                // Make sure an Actor doesn't appear twice in the list because he or she plays multiple characters.
                tVShow.Cast = tVShow.Cast.GroupBy(a => a.Id).Select(ac => ac.First()).ToList();

                scraperDbContext.TVShows.Add(tVShow);
                scraperDbContext.SaveChanges();
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
