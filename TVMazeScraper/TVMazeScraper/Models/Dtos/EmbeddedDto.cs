using System.Collections.Generic;

namespace TVMazeScraper.Models.Dtos
{
    public class EmbeddedDto
    {
        public IEnumerable<CastMemberDto> Cast { get; set; }
    }
}
