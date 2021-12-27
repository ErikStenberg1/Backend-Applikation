using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Data;
using WebApplication.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
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

        public Movie movie { get; set; }
        public Actor actor { get; set; }
        public MovieCast movieCast { get; set; }
        public Review review { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            movie = await database.Movie.FirstOrDefaultAsync(m => m.ID == id);

            if (movie == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
