
using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.Songs
{
    public class CreateModel : PageModel
    {
        private readonly SongService _songService;   // was SongRepository

        // dependency injection
        public CreateModel(SongService songService)
        {
            _songService = songService;
        }

        //private readonly SongRepository _songRepository;

        ////  dependancy injection
        //public CreateModel(SongRepository songRepository)
        //{
        //    _songRepository = songRepository;
        //}

        // zonder depdancy injection
        //public IndexModel()
        //{
        //    _songRepository = new SongRepository();
        //}

        // connect data van submitted form values
        [BindProperty]
        public Song Song { get; set; } = new Song();
        // Todo: request model gebruiken voor formulier request DTO model
        // Todo: research overposting
        // post request. 
        public async Task<IActionResult> OnPostAsync()
        {
            // controleert of de ingevulde form geldig is
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // async. de thread kan ondertussen ander werk uitvoeren.

            // gebruiker A voegt een song toe aan de database
            // gebruiker B opent de overzichtspagina met alle songs
            // gebruiker C bekijkt een andere pagina
            // await zorgt ervoor dat de code wacht tot de databasebewerking klaar is voordat de volgende regel code wordt uitgevoerd
            await _songService.AddAsync(Song);

            return RedirectToPage("./Index");
        }
    }
}