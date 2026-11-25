using Application.Models;

namespace Application.Interfaces
{
    public interface IArtistRepository
    {
        List<Artist> GetAll();
        //Task<int> AddArtistAsync(Song song);
        //Task<int> DeleteArtistAsync(int id);
    }
}
