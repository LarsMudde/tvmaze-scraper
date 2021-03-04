using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TVMazeScraper.Models
{
    public class Actor
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public ICollection<TVShow> TVShows { get; set; }
    }
}
