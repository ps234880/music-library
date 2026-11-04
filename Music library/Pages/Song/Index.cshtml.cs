using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Music_library.Repository;
using System.Reflection;

namespace Music_library.Pages.Song
{
    public class IndexModel : PageModel
    {
        private readonly SongRepository _songRepository;

        // voorbeeld van solid principe D. Dependency Inversion Principle. high/low level code verdelen
        public IndexModel(SongRepository songRepository)
        {
            _songRepository = songRepository;
        }

        public List<Model.Song> Songs { get; private set; } = [];

        public void OnGet()
        {
            Songs = _songRepository.GetAll();
        }
    }
}
