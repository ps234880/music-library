
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;
using Music_library.Model;
using Music_library.Repository;

namespace Dashboard_Sales_Dev.Pages.Quotation
{
    public class CreateModel : PageModel
    {
        private readonly SongRepository _songRepository;

        // voorbeeld van solid principe D. Dependency Inversion Principle. high/low level code verdelen
        public CreateModel(SongRepository songRepository)
        {
            _songRepository = songRepository;
        }

        // connect data van submitted form values
        [BindProperty]
        public Song Song { get; set; } = new Song();

        //public void OnGet()
        //{
        //}

        public async Task<IActionResult> OnPostAsync()
        {
            // geeft dezelfde pagina weer terug
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _songRepository.AddSongAsync(Song);

            return RedirectToPage("./Index");
        }
    }
}