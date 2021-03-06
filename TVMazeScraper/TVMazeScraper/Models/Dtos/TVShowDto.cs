namespace TVMazeScraper.Models.Dtos
{
    public class TVShowDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public EmbeddedDto _embedded { get; set; }
    }
}
