using Application.Models;

namespace Application.Interfaces
{
    public interface ISongRepository
    {
        List<Song> GetAll();

        Task<Song?> GetByIdAsync(int id);

        Task<int> UpdateSongAsync(Song song);

        Task<int> AddSongAsync(Song song);

        Task<int> DeleteSongAsync(int id);
    }
}
