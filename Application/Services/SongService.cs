using Application.Interfaces;
using Application.Models;

namespace Application.Services
{
    public class SongService
    {
        private readonly ISongRepository _songRepository;

        public SongService(ISongRepository songRepository)
        {
            _songRepository = songRepository;
        }

        public List<Song> GetAll()
        {
            return _songRepository.GetAll();
        }

        public async Task<int> AddAsync(Song song)
        {
            if (string.IsNullOrWhiteSpace(song.Title))
            {
                throw new ArgumentException("Een nummer heeft een titel nodig.");
            }
            if (string.IsNullOrWhiteSpace(song.Genre))
            {
                throw new ArgumentException("Een nummer heeft een genre nodig.");
            }
            if (song.DurationInSeconds <= 0)
            {
                throw new ArgumentException("De duur moet groter dan nul zijn.");
            }

            return await _songRepository.AddSongAsync(song);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _songRepository.DeleteSongAsync(id);
        }
    }
}
