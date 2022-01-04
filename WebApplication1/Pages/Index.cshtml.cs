using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;
using WebApplication1.Data;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext database;

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext database)
        {
            _logger = logger;
            this.database = database;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!database.Movie.Any())
            {
                var brad = new Actor() { FirstName = "Brad", LastName = "Pitt", Age = 50 };
                var avengers = new Movie() { Title = "Avengers", PosterPath = "brad.jpg", Category = "Action", ReleaseYear = 2020, Actors = new() { brad } };
                database.AddRange(brad, avengers);
                await database.SaveChangesAsync();
            }
            return Page();

        }
    }

}
