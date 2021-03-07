using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TVMazeScraper.Models;

namespace TVMazeScraper.Repositories
{
    public class ScraperRepository : IScraperRepository
    {
        private readonly ScraperDbContext _context;
        private readonly ILogger<ScraperRepository> _logger;

        public ScraperRepository(ScraperDbContext context, ILogger<ScraperRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Gets a list of tvshows including cast where the name contains the searchterm
        /// </summary>
        /// <param name="searchTerm">Word or part of word to search for</param>
        /// <param name="page">Which page to get</param>
        /// <param name="pageSize">How many results per page are desired</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>IEnumerable with TVShows and cast matching searchterm</returns>
        public async Task<IEnumerable<TVShow>> SearchShowsWithCast(string searchTerm, uint page, uint pageSize, CancellationToken cancellationToken)
        {
            return await _context.TVShows
                .Where(s => s.Name.Contains(searchTerm))
                .Include(s => s.Cast)
                .ThenInclude(a => a.Actor)
                .OrderBy(s => s.Id)
                .Skip((int) (page * pageSize))
                .Take((int) pageSize)
                .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Saves or updates a TVShow with Cast
        /// </summary>
        /// <param name="tVShow">A TVShow only</param>
        /// <param name="cast">A list of Actors to save</param>
        /// <returns>Task</returns>
        public async Task SaveOrUpdateTVShowWithCast(TVShow tVShow, IEnumerable<Actor> cast)
        {
            // Make sure an Actor doesn't appear twice in the list because he or she plays multiple characters.
            cast = cast.GroupBy(a => a.Id).Select(ac => ac.First()).ToList();
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await SaveTvShow(tVShow);
                    await SaveCastWithTvShow(cast, tVShow);
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    _logger.LogError("Repository error: {message}", e.Message);
                }
            }
        }

        private async Task SaveCastWithTvShow(IEnumerable<Actor> cast, TVShow tVShow)
        {
            try
            {
                foreach (Actor actor in cast)
                {
                    var _actor = _context.Actors.FirstOrDefault(item => item.Id == actor.Id);
                    if (_actor == null)
                    {
                        await _context.Actors.AddAsync(actor);
                        await _context.ActorsTVShows.AddAsync(new ActorTVShow(actor.Id, tVShow.Id));
                    }
                    else if (!_context.ActorsTVShows.Any(sa => sa.TVShowId == tVShow.Id && sa.ActorId == actor.Id))
                    {
                        await _context.ActorsTVShows.AddAsync(new ActorTVShow(actor.Id, tVShow.Id));
                    }
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(
                    "Repository error: {message}", e.Message);
            }
        }

        private async Task SaveTvShow(TVShow tVShow)
        {
            try
            {
                var _tVShow = _context.TVShows.FirstOrDefault(item => item.Id == tVShow.Id);
                if (_tVShow == null)
                {
                    await _context.TVShows.AddAsync(tVShow);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(
                    "Repository error: {message}", e.Message);
            }
        }
    }
}
