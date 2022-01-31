using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication.Models;
using WebApplication1.Data;

namespace WebApplication1.Pages.Movies
{
    public class AddActorModel : PageModel
    {
        private readonly ApplicationDbContext database;

        public AddActorModel(ApplicationDbContext database)
        {
            this.database = database;
        }
        public Movie Movie { get; set; }
        public List<SelectListItem> ActorList { get; set; }
        [FromQuery]
        public Actor Actor { get; set; }
        public async Task LoadMovie(int id)
        {
            Movie = await database.Movie
                .Where(m => m.ID == id)
                .SingleOrDefaultAsync();
        }
        public async Task OnGetAsync(int id)
        {
            ActorList = await database.Actor.AsNoTracking()
            .Where(a => a.Movies.All(m => m.ID != id))
            .OrderBy(a => a.FirstName)
            .ThenBy(a => a.LastName)
            .Select(a => new SelectListItem
            {
                Value = a.ID.ToString(),
                Text = a.FullName
            })
            .ToListAsync();
        }
        public async Task<IActionResult> OnPostAsync(int id, Movie movie, Actor actor)
        {
            await LoadMovie(id);
            var selectedActor = await database.Actor.FindAsync(actor.ID);
            Movie.Actors = new List<Actor>();
            Movie.Actors.Add(selectedActor);

            await database.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
