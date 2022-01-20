using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Data;
using WebApplication.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
        private void CreateEmptyMovie()
        {
            Movie = new Movie
            {
                
            };
        }
        public async Task<IActionResult> OnPostAsync(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                //return Page();
            }
            CreateEmptyMovie();
            Movie.Title = movie.Title;
            Movie.ReleaseYear = movie.ReleaseYear;
            Movie.PosterPath = movie.PosterPath;
            Movie.Category = movie.Category;

            database.Movie.Add(Movie);
            await database.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
