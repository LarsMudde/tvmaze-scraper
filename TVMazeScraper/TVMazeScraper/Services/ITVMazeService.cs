using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TVMazeScraper.Models;

namespace TVMazeScraper.Services
{
    public interface ITVMazeService
    {
        public Task<ActorDto> GetActorByIdAsync(long id);
    }
}
