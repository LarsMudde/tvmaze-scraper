using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TVMazeScraper.Models
{
    public class TVShow
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Actor> Cast { get; set; }
    }
}
