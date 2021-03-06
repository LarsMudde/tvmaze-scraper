using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TVMazeScraper.Models.Dtos
{
    public class ActorResponseDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }
    }
}
