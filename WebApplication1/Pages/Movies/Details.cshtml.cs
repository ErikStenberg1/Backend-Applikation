using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Data;
using WebApplication.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Migrations;

namespace WebApplication1.Pages.Movies
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext database;
        public DetailsModel(ApplicationDbContext database)
        {
            this.database = database;
        }
        public Actor Actor { get; set; }
        public Movie Movie { get; set; }
        public List<Actor> Actors { get; set; }
        public List<Review> Reviews { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Movie = await database.Movie.FirstOrDefaultAsync(m => m.ID == id);

            Actors = await database.Actor
                .Include(actor => actor.Movies)
                .Where(actor => actor.Movies.Any(movie => movie.ID == id))
                .ToListAsync();

            Reviews = await database.Review
                .Where(review => review.Movie.ID == id)
                .ToListAsync();
                
            if (Movie == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
