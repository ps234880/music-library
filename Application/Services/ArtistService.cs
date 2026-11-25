using Application.Interfaces;
using Application.Models;

namespace Application.Services
{
    public class ArtistService
    {
        private readonly IArtistRepository _artistRepository;

        public ArtistService(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public List<Artist> GetAll()
        {
            return _artistRepository.GetAll();
        }

        public async Task<Artist?> GetByIdAsync(int id)
        {
            return await _artistRepository.GetByIdAsync(id);
        }

        public async Task<int> AddAsync(Artist artist)
        {
            return await _artistRepository.AddArtistAsync(artist);
        }

        public async Task<int> UpdateAsync(Artist artist)
        {
            return await _artistRepository.UpdateArtistAsync(artist);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _artistRepository.DeleteArtistAsync(id);
        }
    }
}
