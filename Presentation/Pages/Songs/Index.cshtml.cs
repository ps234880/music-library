using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Persistence.Repository;
using System.Reflection;

namespace Presentation.Pages.Songs
{
    public class IndexModel : PageModel
    {
        private readonly SongRepository _songRepository;

        // dependancy injection. is nog niet volledig
        public IndexModel(SongRepository songRepository)
        {
            _songRepository = songRepository;
        }

        // zonder depdancy injection
        //public IndexModel()
        //{
        //    _songRepository = new SongRepository();
        //}

        // alleen eigen class mag dingen wijzigen
        public List<Domain.Model.Song> Songs { get; private set; } = new List<Domain.Model.Song>();

        public void OnGet()
        {
            Songs = _songRepository.GetAll();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _songRepository.DeleteSongAsync(id);

            return RedirectToPage();
        }
    }
}
