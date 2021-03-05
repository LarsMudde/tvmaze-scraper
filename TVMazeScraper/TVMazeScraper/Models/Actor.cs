using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TVMazeScraper.Models
{
    public class Actor
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }
        [JsonIgnore]
        public virtual ICollection<TVShow> TVShows { get; set; }
    }
}
