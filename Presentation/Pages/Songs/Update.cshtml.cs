using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Presentation.Pages.Songs
{
    public class UpdateModel : PageModel
    {
        private readonly SongService _songService;
        private readonly ArtistService _artistService;

        // Dependency injection
        public UpdateModel(SongService songService, ArtistService artistService)
        {
            _songService = songService;
            _artistService = artistService;
        }

        // Connect data from submitted form values
        [BindProperty]
        public Song Song { get; set; } = new Song();

        public List<SelectListItem> Artists { get; set; } = new List<SelectListItem>();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Get the song by ID
            Song? song = await _songService.GetByIdAsync(id);

            if (song == null)
            {
                return NotFound();
            }

            Song = song;

            LoadArtists();

            return Page();
        }

        // Post request
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                LoadArtists();
                return Page();
            }

            await _songService.UpdateAsync(Song);

            return RedirectToPage("./Index");
        }

        private void LoadArtists()
        {
            // Get all artists from the ArtistService
            List<Artist> artists = _artistService.GetAll();

            // Create an empty list for the dropdown
            Artists = new List<SelectListItem>();

            // Loop through all artists
            foreach (Artist artist in artists)
            {
                // Create a dropdown item for each artist
                SelectListItem item = new SelectListItem();

                // The artist ID is used as the value
                item.Value = artist.Id.ToString();

                // The artist name is displayed to the user
                item.Text = artist.Name;

                // Add the item to the dropdown list
                Artists.Add(item);
            }
        }
    }
}