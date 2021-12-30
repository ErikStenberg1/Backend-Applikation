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
            var erik = new Actor() { FirstName = "Erik", LastName = "Stenberg", Age = 19 };
            var avengers = new Movie() { Title = "Avengers", PosterPath = "brad.jpg", Category = "Action", ReleaseYear = 2020, Actors = new() { erik } };
            database.AddRange(erik, avengers);
            await database.SaveChangesAsync();
            return RedirectToPage("/Movies/Index");
        }
    }
    
}
