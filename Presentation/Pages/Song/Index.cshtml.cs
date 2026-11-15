using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Music_library.Repository;
using System.Reflection;

namespace Music_library.Pages.Song
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
        public List<Model.Song> Songs { get; private set; } = new List<Model.Song>();

        public void OnGet()
        {
            Songs = _songRepository.GetAll();
        }
    }
}
