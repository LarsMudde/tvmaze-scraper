using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TVMazeScraper.Models
{
    public class Actor
    {
        [KeyAttribute()]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }
        public virtual IEnumerable<TVShow> TVShows { get; set; }
    }
}
