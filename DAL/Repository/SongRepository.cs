using Microsoft.Extensions.Configuration;
using Music_library.Model;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System.Reflection;

namespace Music_library.Repository
{
    public class SongRepository
    {
        private readonly string _connectionString;

        // constructor met de interface configuratie van databaseconnectie
        public SongRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }
        // dit kan hardcoded zijn

        public List<Song> GetAll()
        {
            List<Song> songs = new List<Song>();

            // opent connectie
            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            // stel dat je miljoenen songs hebt. paging offset. als het meer dan 2 paginas zijn offset gebruiken
            string query =
                @"SELECT id, artist_id, title, genre, durationInSeconds, image, file 
            FROM song";

            // sql command aanmaken en uitvoeren
            using MySqlCommand command = new MySqlCommand(query, connection);
            using MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                // elke row word convert naar object song object
                Song song = new Song
                {
                    Id = reader.GetInt32("id"),
                    ArtistId = reader.GetInt32("artist_id"),
                    Title = reader.GetString("title"),
                    Genre = reader.GetString("genre"),
                    DurationInSeconds = reader.GetInt32("durationInSeconds"),
                    Image = reader.GetString("image"),
                    File = reader.GetString("file")
                };
                // voeg object aan list
                songs.Add((song));
            }

            return songs;
        }

        public async Task<int> AddSongAsync(Song song)
        {
            // opent database asyncrhoom 
            using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync(); // dit spreekt miss tegen

            var sql = @"
        INSERT INTO song
            (artist_id, title, genre, durationInSeconds, image, file)
        VALUES
            (@artist_id, @title, @genre, @durationInSeconds, @image, @file);";

            // create sql command
            using var cmd = new MySqlCommand(sql, connection);

            // add parameters
            cmd.Parameters.AddWithValue("@artist_id", song.ArtistId);
            cmd.Parameters.AddWithValue("@title", song.Title);
            cmd.Parameters.AddWithValue("@genre", song.Genre);
            cmd.Parameters.AddWithValue("@durationInSeconds", song.DurationInSeconds);
            cmd.Parameters.AddWithValue("@image", song.Image);
            cmd.Parameters.AddWithValue("@file", song.File);

            // execute insert statement
            return await cmd.ExecuteNonQueryAsync();
        }
    }
}
