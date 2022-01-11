using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication.Models;
using WebApplication1.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext database;
        private readonly AccessControl accessControl;
        public IndexModel(ApplicationDbContext database, AccessControl accessControl)
        {
            this.database = database;
            this.accessControl = accessControl;
        }

        public IList<Movie> Movies { get; set; }
        [FromQuery]
        public string SearchTerm { get; set; }
        public Review Review { get; set; }
        public double? avgScore { get; set; }

        public async Task OnGetAsync()
        {
            var query = database.Movie.AsNoTracking();
            if (SearchTerm != null)
            {
                query = query.Where(m =>
                m.Title.ToLower().Contains(SearchTerm.ToLower()));
            }
            avgScore = await database.Review
                .Include(m => m.Movie)
                .Where(r => r.Movie.ID == r.MovieID)
                .AverageAsync(r => r.Score);

            Movies = await query.ToListAsync();
        }
    }
}
