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

        public async Task OnGetAsync()
        {
            var query = database.Movie.AsNoTracking();
            if (SearchTerm != null)
            {
                query = query.Where(m =>
                m.Title.ToLower().Contains(SearchTerm.ToLower()));
            }


            Movies = await query.ToListAsync();

            foreach (var movie in Movies)
            {
                movie.AvgScore = await GetAvgScore(movie);
            }
        }
        public async Task<double> GetAvgScore(Movie movie)
        {
            var reviews = await database.Review.Include(m => m.Movie)
                .Where(r => r.MovieID == movie.ID)
                .ToListAsync();
            double totalScore = 0;
            foreach (var review in reviews)
            {
                totalScore += (double)review.Score;
            }
            var avgScore = totalScore / reviews.Count;
            return double.IsNaN(avgScore) ? 0 : avgScore;
        }
    }
}
