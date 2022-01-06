using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Data;
using WebApplication.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WebApplication1.Pages.Reviews
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext database;
        private readonly AccessControl accessControl;

        public CreateModel(ApplicationDbContext database, AccessControl accessControl)
        {
            this.database = database;
            this.accessControl = accessControl;
        }
        public Review Review { get; set; }
        public Movie Movie { get; set; }
        public List<Review> Reviews { get; set; }
        public int movieID { get; set; }
        public int ReviewID { get; set; }
        public int MovieID { get; private set; }
        //movieID is 0
        private void CreateEmptyReview(int movieID)
        {
            Review = new Review
            {
                UserID = accessControl.LoggedInUserID,
                MovieID = movieID
            };
        }
        public async Task OnGetAsync(int movieID)
        {
            Reviews = await database.Review.Where(r => r.MovieID == movieID && r.UserID == accessControl.LoggedInUserID).ToListAsync();

            ReviewID = await database.Review
            .Where(r => r.UserID == accessControl.LoggedInUserID && r.MovieID == r.Movie.ID)
            .Select(r => r.ID).SingleOrDefaultAsync();

            CreateEmptyReview(movieID);
        }
        public async Task<IActionResult> OnPostAsync(Review review, int movieID)
        {
            Reviews = await database.Review.Where(r => r.MovieID == movieID && r.UserID == accessControl.LoggedInUserID).ToListAsync();
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            else if (Reviews.Any())
            {
                ReviewID = await database.Review
                .Where(r => r.UserID == accessControl.LoggedInUserID && r.MovieID == r.Movie.ID)
                .Select(r => r.ID).SingleOrDefaultAsync();
                return RedirectToPage("./Edit", new { id = ReviewID });
            }
            //some error here
            MovieID = await database.Movie
            .Where(m => m.ID == Review.MovieID)
            .Select(m => m.ID)
            .SingleOrDefaultAsync();

            CreateEmptyReview(movieID);

            Review.Text = review.Text;
            Review.Score = review.Score;


            await database.Review.AddAsync(review);
            await database.SaveChangesAsync();
            return RedirectToPage("./Index", new { id = movieID });
        }
    }
}
