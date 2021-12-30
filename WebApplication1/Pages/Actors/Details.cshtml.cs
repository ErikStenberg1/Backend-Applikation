using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Data;
using WebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Pages.Actors
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
        public List<Movie> Movies { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Actor = await database.Actor.FirstOrDefaultAsync(a => a.ID == id);

            Movies = await database.Movie
                .Include(movie => movie.Actors)
                .Where(movie => movie.Actors.Any(actor => actor.ID == id))
                .ToListAsync();

            if (Actor == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
