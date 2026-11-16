namespace Application.Model
{
    public class Song
    {
        // hier validatie toevoegen
        public int Id { get; set; }

        public string ArtistName { get; set; }

        public string Title { get; set; }

        public string Genre { get; set; }

        public int DurationInSeconds { get; set; }

        public string Image { get; set; }

        public string? File { get; set; }
    }
}
