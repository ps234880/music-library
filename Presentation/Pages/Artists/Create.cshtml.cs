using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.Artists
{
    public class CreateModel : PageModel
    {
        private readonly ArtistService _artistService;

        public CreateModel(ArtistService artistService)
        {
            _artistService = artistService;
        }

        [BindProperty]
        public Artist Artist { get; set; } = new Artist();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _artistService.AddAsync(Artist);

            return RedirectToPage("./Index");
        }
    }
}
