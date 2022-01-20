using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication.Models;
using WebApplication1.Data;

namespace WebApplication1.Pages.Movies
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext database;
        private readonly AccessControl accessControl;

        public EditModel(ApplicationDbContext database, AccessControl accessControl)
        {
            this.database = database;
            this.accessControl = accessControl;
        }
        public Movie Movie { get; set; }
        public async Task LoadMovie(int id)
        {
            Movie = await database.Movie
                .Where(m => m.ID == id)
                .SingleOrDefaultAsync();
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            await LoadMovie(id);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id, Movie movie)
        {
            await LoadMovie(id);
            Movie.Title = movie.Title;
            Movie.ReleaseYear = movie.ReleaseYear;
            Movie.PosterPath = movie.PosterPath;

            await database.SaveChangesAsync();
            return RedirectToPage("./Index");

        }
    }
}
