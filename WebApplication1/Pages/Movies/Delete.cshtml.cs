using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Data;
using WebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Pages.Movies
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext database;

        public DeleteModel(ApplicationDbContext database)
        {
            this.database = database;
        }
        public Movie Movie { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Movie = await database.Movie
                .Where(m => m.ID == id)
                .SingleOrDefaultAsync();

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            Movie = await database.Movie
                .Where(m => m.ID == id)
                .SingleOrDefaultAsync();

            var movie = await database.Movie.FindAsync(id);

            database.Movie.Remove(movie);
            await database.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
