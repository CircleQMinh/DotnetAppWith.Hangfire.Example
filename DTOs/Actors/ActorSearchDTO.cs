namespace DotnetAppWith.Hangfire.Example.DTOs.Actors
{
    public class ActorSearchDTO
    {
        public string Keyword { get; set; } = string.Empty;
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
