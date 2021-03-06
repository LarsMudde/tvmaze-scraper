using Microsoft.EntityFrameworkCore;
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

        public ScraperRepository(ScraperDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TVShow>> SearchShowsWithCast(string query, int page, int pagesize, CancellationToken cancellationToken)
        {
            return await _context.TVShows
                .Where(s => s.Name.Contains(query))
                .Include(s => s.Cast)
                .ThenInclude(a => a.Actor)
                .OrderBy(s => s.Id)
                .Skip(page * pagesize)
                .Take(pagesize)
                .ToListAsync(cancellationToken);
        }

        public async Task SaveOrUpdateTVShowWithCast(TVShow tVShow, IEnumerable<Actor> cast)
        {
            // Make sure an Actor doesn't appear twice in the list because he or she plays multiple characters.
            cast = cast.GroupBy(a => a.Id).Select(ac => ac.First()).ToList();
            using (var transaction = _context.Database.BeginTransaction())
            {
                await SaveTvShow (tVShow);
                await SaveCastWithTvShow(cast, tVShow);
                transaction.Commit();
            }
        }

        private async Task SaveCastWithTvShow(IEnumerable<Actor> cast, TVShow tVShow)
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

        private async Task SaveTvShow(TVShow tVShow)
        {
            var _tVShow = _context.TVShows.FirstOrDefault(item => item.Id == tVShow.Id);
            if (_tVShow == null)
            {
                await _context.TVShows.AddAsync(tVShow);
                await _context.SaveChangesAsync();
            }

        }
    }
}
