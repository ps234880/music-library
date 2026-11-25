using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.Artists
{
    public class IndexModel : PageModel
    {
        private readonly ArtistService _artistService;

        public IndexModel(ArtistService artistService)
        {
            _artistService = artistService;
        }

        public List<Artist> Artists { get; private set; } = new List<Artist>();

        public void OnGet()
        {
            Artists = _artistService.GetAll();
        }
    }
}

