using Application.Interfaces;
using Application.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Persistence.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly string _connectionString;

        public ArtistRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public List<Artist> GetAll()
        {
            List<Artist> artists = new List<Artist>();

            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            string query = @"
                SELECT id, name, description
                FROM artist
                ORDER BY name";

            using MySqlCommand command = new MySqlCommand(query, connection);
            using MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Artist artist = new Artist
                {
                    Id = reader.GetInt32("id"),
                    Name = reader.GetString("name"),
                    Description = reader.GetString("description")
                };

                artists.Add(artist);
            }

            return artists;
        }

        public async Task<Artist?> GetByIdAsync(int id)
        {
            using MySqlConnection connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();

            string query = @"
                SELECT id, name, description
                FROM artist
                WHERE id = @id";

            using MySqlCommand command = new MySqlCommand(query, connection);

            command.Parameters.AddWithValue("@id", id);

            using MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                Artist artist = new Artist
                {
                    Id = reader.GetInt32("id"),
                    Name = reader.GetString("name"),
                    Description = reader.GetString("description")
                };

                return artist;
            }

            return null;
        }

        public async Task<int> AddArtistAsync(Artist artist)
        {
            using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();

            var sql = @"
                INSERT INTO artist (id, name, description)
                VALUES (@id, @name, @description)";

            using var cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@id", artist.Id);
            cmd.Parameters.AddWithValue("@name", artist.Name);
            cmd.Parameters.AddWithValue("@description", artist.Description);

            return await cmd.ExecuteNonQueryAsync();
        }

        public async Task<int> UpdateArtistAsync(Artist artist)
        {
            using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();

            var sql = @"
                UPDATE artist
                SET name = @name, description = @description
                WHERE id = @id";

            using var cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@id", artist.Id);
            cmd.Parameters.AddWithValue("@name", artist.Name);
            cmd.Parameters.AddWithValue("@description", artist.Description);

            return await cmd.ExecuteNonQueryAsync();
        }

        public async Task<int> DeleteArtistAsync(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();

            var sql = @"
                DELETE FROM artist
                WHERE id = @id";

            using var cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@id", id);

            return await cmd.ExecuteNonQueryAsync();
        }
    }
}