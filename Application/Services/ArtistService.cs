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
    }
}
