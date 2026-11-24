using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.Songs
{
    public class IndexModel : PageModel
    {
        private readonly SongService _songService;

        public IndexModel(SongService songService)
        {
            _songService = songService;
        }
        //private readonly SongRepository _songRepository;

        // dependancy injection. is nog niet volledig
        //public IndexModel(SongRepository songRepository)
        //{
        //    _songRepository = songRepository;
        //}

        // zonder depdancy injection
        //public IndexModel()
        //{
        //    _songRepository = new SongRepository();
        //}

        // alleen eigen class mag dingen wijzigen

        public List<Song> Songs { get; private set; } = new List<Song>();
        //public IReadOnlyList<Song> Songs { get; private set; } = new List<Song>();
        public void OnGet()
        {
            Songs = _songService.GetAll();
        }
        //public void OnGet()
        //{
        //    Songs = _songRepository.GetAll();
        //}

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _songService.DeleteAsync(id);

            return RedirectToPage();
        }
    }
}
