namespace Application.Models
{
    public class Song
    {
        // hier validatie toevoegen
        public int Id { get; set; }

        public int ArtistId { get; set; }

        public string ArtistName { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string Genre { get; set; } = string.Empty;

        public int DurationInSeconds { get; set; }

        public string Image { get; set; } = string.Empty;

        public string File { get; set; } = string.Empty;
    }
}
