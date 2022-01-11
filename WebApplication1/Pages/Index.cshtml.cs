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
                var harrison = new Actor() { FirstName = "Harrison", LastName = "Ford", Age = 75 };
                var morgan = new Actor() { FirstName = "Morgan", LastName = "Freeman", Age = 70 };
                var tom = new Actor() { FirstName = "Tom", LastName = "Hanks", Age = 60 };
                var avengers = new Movie() { Title = "Avengers", PosterPath = "brad.jpg", Category = "Action", ReleaseYear = 2020, Actors = new() { brad, harrison, morgan } };
                var valentine = new Movie() { Title = "Valentine's Day", PosterPath = "brad.jpg", Category = "Comedy", ReleaseYear = 2019, Actors = new() { morgan, tom, brad } };
                database.AddRange(brad, harrison, morgan, tom, avengers,valentine);
                await database.SaveChangesAsync();
            }
            return Page();

        }
    }

}
