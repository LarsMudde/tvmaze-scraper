namespace TVMazeScraper.Models
{
    public class ActorTVShow
    {
        public ActorTVShow() { }
        public ActorTVShow(long actorId, long tVShowId)
        {
            ActorId = actorId;
            TVShowId = tVShowId;
        }


        public long ActorId { get; set; }
        public Actor Actor { get; set; }

        public long TVShowId { get; set; }
        public TVShow TVShow { get; set; }
    }
}
