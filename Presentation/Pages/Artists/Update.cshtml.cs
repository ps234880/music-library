using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.Artists
{
    public class UpdateModel : PageModel
    {
        private readonly ArtistService _artistService;

        // Dependency injection
        public UpdateModel(ArtistService artistService)
        {
            _artistService = artistService;
        }

        // Connect data from submitted form values
        [BindProperty]
        public Artist Artist { get; set; } = new Artist();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Get the artist by ID
            Artist? artist = await _artistService.GetByIdAsync(id);

            if (artist == null)
            {
                return NotFound();
            }

            Artist = artist;

            return Page();
        }

        // Post request
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _artistService.UpdateAsync(Artist);

            return RedirectToPage("./Index");
        }
    }
}