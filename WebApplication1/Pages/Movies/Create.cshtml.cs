using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Data;
using WebApplication.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication1.Pages.Movies
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext database;

        public CreateModel(ApplicationDbContext database)
        {
            this.database = database;
        }
        public Movie Movie { get; set; }
        public List<SelectListItem> ActorList { get; set; }
        [FromQuery]
        public Actor Actor { get; set; }
        public async Task Setup()
        {
            ActorList = await database.Actor.AsNoTracking()
            .OrderBy(a => a.FirstName)
            .ThenBy(a => a.LastName)
            .Select(a => new SelectListItem
            {
                Value = a.ID.ToString(),
                Text = a.FullName
            })
            .ToListAsync();
        }
        private void CreateEmptyMovie()
        {
            Movie = new Movie
            {
                
            };
        }
        public async Task<IActionResult> OnGetAsync()
        {
            await Setup();
            CreateEmptyMovie();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(Movie movie, Actor actor)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                //return Page();
            }
            await Setup();
            var selectedActor = await database.Actor.FindAsync(actor.ID);
            CreateEmptyMovie();
            Movie.Title = movie.Title;
            Movie.ReleaseYear = movie.ReleaseYear;
            Movie.PosterPath = movie.PosterPath;
            Movie.Category = movie.Category;
            Movie.Actors = new List<Actor>();
            Movie.Actors.Add(selectedActor);

            database.Movie.Add(Movie);
            await database.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
