using Application.Models;

namespace Application.Interfaces
{
    public interface ISongRepository
    {
        List<Song> GetAll();
        Task<int> AddSongAsync(Song song);
        Task<int> DeleteSongAsync(int id);
    }
}
