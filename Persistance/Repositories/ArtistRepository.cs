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
    }
}