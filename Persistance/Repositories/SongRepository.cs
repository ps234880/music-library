using Application.Interfaces;
using Application.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Persistence.Repositories
{
    public class SongRepository : ISongRepository
    {
        private readonly string _connectionString;

        // Constructor met de interface configuratie van databaseconnectie
        public SongRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        //public SongRepository()
        //{
        //    _connectionString = "Server=localhost;Database=musiclibrary;Uid=root;Pwd=password;";
        //}

        public List<Song> GetAll()
        {
            List<Song> songs = new List<Song>();

            // Opent connectie
            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            // Todo: tabel alias
            // Stel dat je miljoenen songs hebt. Paging offset. Als het meer dan 2 paginas zijn offset gebruiken
            string query = @"
                SELECT song.id, artist.name, song.title, song.genre, song.durationInSeconds, song.image, song.file
                FROM song
                INNER JOIN artist ON song.artist_id = artist.id
                ORDER BY song.id";

            // SQL command aanmaken en uitvoeren
            using MySqlCommand command = new MySqlCommand(query, connection);
            using MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                // Elke row wordt convert naar object song object
                Song song = new Song
                {
                    Id = reader.GetInt32("id"),
                    //ArtistId = reader.GetInt32("artist_id"),
                    ArtistName = reader.GetString("name"),
                    Title = reader.GetString("title"),
                    Genre = reader.GetString("genre"),
                    DurationInSeconds = reader.GetInt32("durationInSeconds"),
                    Image = reader.GetString("image"),
                    File = reader.GetString("file")
                };

                // Voeg object aan list
                songs.Add((song));
            }

            return songs;
        }

        public async Task<Song?> GetByIdAsync(int id)
        {
            using MySqlConnection connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();

            string query = @"
                SELECT id, artist_id, title, genre, durationInSeconds, image, file
                FROM song
                WHERE id = @id";

            using MySqlCommand cmd = new MySqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@id", id);

            using MySqlDataReader reader = (MySqlDataReader)await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
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

                return song;
            }

            return null;
        }

        public async Task<int> AddSongAsync(Song song)
        {
            // Open de databaseverbinding asynchroon.
            // Het programma wacht totdat de verbinding geopend is,
            // maar de huidige thread kan ondertussen ander werk uitvoeren.
            // Await wacht totdat de verbinding geopend is, zonder de huidige thread te blokkeren.
            using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();

            var sql = @"
                INSERT INTO song
                (artist_id, title, genre, durationInSeconds, image, file)
                VALUES
                (@artist_id, @title, @genre, @durationInSeconds, @image, @file)";

            // Create SQL command
            using var cmd = new MySqlCommand(sql, connection);

            // Add parameters
            cmd.Parameters.AddWithValue("@artist_id", song.ArtistId);
            cmd.Parameters.AddWithValue("@title", song.Title);
            cmd.Parameters.AddWithValue("@genre", song.Genre);
            cmd.Parameters.AddWithValue("@durationInSeconds", song.DurationInSeconds);
            cmd.Parameters.AddWithValue("@image", song.Image);
            cmd.Parameters.AddWithValue("@file", song.File);

            // Execute insert statement
            return await cmd.ExecuteNonQueryAsync();
        }

        public async Task<int> UpdateSongAsync(Song song)
        {
            using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();

            string query = @"
                UPDATE song
                SET artist_id = @artist_id,
                    title = @title,
                    genre = @genre,
                    durationInSeconds = @durationInSeconds,
                    image = @image,
                    file = @file
                WHERE id = @id";

            using MySqlCommand cmd = new MySqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@id", song.Id);
            cmd.Parameters.AddWithValue("@artist_id", song.ArtistId);
            cmd.Parameters.AddWithValue("@title", song.Title);
            cmd.Parameters.AddWithValue("@genre", song.Genre);
            cmd.Parameters.AddWithValue("@durationInSeconds", song.DurationInSeconds);
            cmd.Parameters.AddWithValue("@image", song.Image);
            cmd.Parameters.AddWithValue("@file", song.File);

            return await cmd.ExecuteNonQueryAsync();
        }
        public async Task<int> DeleteSongAsync(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();

            var sql = @"
                DELETE FROM song
                WHERE id = @id";

            using var cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@id", id);

            return await cmd.ExecuteNonQueryAsync();
        }
    }
}