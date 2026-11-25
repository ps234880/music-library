using Application.Models;

namespace Application.Interfaces
{
    public interface IArtistRepository
    {
        List<Artist> GetAll();

        Task<Artist?> GetByIdAsync(int id);

        Task<int> AddArtistAsync(Artist artist);

        Task<int> UpdateArtistAsync(Artist artist);

        Task<int> DeleteArtistAsync(int id);
    }
}
