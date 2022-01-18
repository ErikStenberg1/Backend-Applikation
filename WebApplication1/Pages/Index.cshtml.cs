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
                var brad = new Actor() { FirstName = "Brad", LastName = "Pitt", Age = 50, PosterPath = "brad.jpg"};
                var harrison = new Actor() { FirstName = "Harrison", LastName = "Ford", Age = 75, PosterPath = "harrison.png"};
                var morgan = new Actor() { FirstName = "Morgan", LastName = "Freeman", Age = 70, PosterPath = "morgan.png"};
                var tom = new Actor() { FirstName = "Tom", LastName = "Hanks", Age = 60, PosterPath = "tom.png"};
                var will = new Actor() { FirstName = "Will", LastName = "Smith", Age = 53, PosterPath = "will.png" };
                var christian = new Actor() { FirstName = "Christian", LastName = "Bale", Age = 47, PosterPath = "christian.png"};
                var avengers = new Movie() { Title = "Avengers", PosterPath = "avenger.jpg", Category = "Action", ReleaseYear = 2020, Actors = new() { brad, harrison, morgan, christian } };
                var valentine = new Movie() { Title = "Valentine's Day", PosterPath = "valentine.jpg", Category = "Comedy", ReleaseYear = 2019, Actors = new() { morgan, tom, brad } };
                var waiting = new Movie() { Title = "Waiting For Forever", PosterPath = "waiting.jpg", Category = "Romance", ReleaseYear = 2011, Actors = new() { tom, harrison, christian } };
                var waitress = new Movie() { Title = "Waitress", PosterPath = "waitress.jpg", Category = "Romance", ReleaseYear = 2007, Actors = new() { tom } };
                var walle = new Movie() { Title = "WALL-E", PosterPath = "walle.jpg", Category = "Animation", ReleaseYear = 2008, Actors = new() { brad } };
                var waterfor = new Movie() { Title = "Water for Elephants", PosterPath = "water.jpg", Category = "Drama", ReleaseYear = 2011, Actors = new() { morgan } };
                var whathappens = new Movie() { Title = "What Happens in Vegas", PosterPath = "whathappens.jpg", Category = "Comedy", ReleaseYear = 2008, Actors = new() { will, christian } };
                var youwillmeet = new Movie() { Title = "You Will Meet a Tall Dark Stranger", PosterPath = "youwill.jpg", Category = "Comedy", ReleaseYear = 2010, Actors = new() { will, brad } };
                database.AddRange(brad,harrison,morgan,will,tom,christian,avengers,valentine,waiting,waitress,walle,waterfor,whathappens,youwillmeet);
                await database.SaveChangesAsync();
            }
            return Page();

        }
    }

}
