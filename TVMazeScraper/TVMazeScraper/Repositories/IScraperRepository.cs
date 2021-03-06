﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TVMazeScraper.Models;

namespace TVMazeScraper.Repositories
{
    public interface IScraperRepository
    {
        Task<IEnumerable<TVShow>> SearchShowsWithCast(string query, int page, int pagesize, CancellationToken cancellationToken);
        Task SaveOrUpdateTVShowWithCast(TVShow tVShow, IEnumerable<Actor> cast);
    }
}