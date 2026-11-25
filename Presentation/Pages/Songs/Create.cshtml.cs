
using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Presentation.Pages.Songs
{
    public class CreateModel : PageModel
    {
        private readonly SongService _songService;   // was SongRepository
        private readonly ArtistService _artistService;

        // dependency injection
        public CreateModel(SongService songService, ArtistService artistService)
        {
            _songService = songService;
            _artistService = artistService;
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

        public List<SelectListItem> Artists { get; set; } = new List<SelectListItem>();

        public void OnGet()
        {
            LoadArtists();
        }

        private void LoadArtists()
        {
            // Get all artists from the ArtistService
            List<Artist> artists = _artistService.GetAll();

            // Create an empty list for the dropdown
            Artists = new List<SelectListItem>();

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
        // Todo: request model gebruiken voor formulier request DTO model
        // Todo: research overposting
        // post request. 
        public async Task<IActionResult> OnPostAsync()
        {
            // controleert of de ingevulde form geldig is
            if (!ModelState.IsValid)
            {
                LoadArtists();
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