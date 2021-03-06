using System.Collections.Generic;

namespace TVMazeScraper.Models.Dtos
{
    public class TVShowResponseDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ActorResponseDto> Cast { get; set; }
    }
}
