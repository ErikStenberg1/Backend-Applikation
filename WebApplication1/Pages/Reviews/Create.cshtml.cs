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
        public int ReviewID { get; set; }
        private void CreateEmptyReview()
        {
            Review = new Review
            {
                UserID = accessControl.LoggedInUserID
            };
        }
        public async Task OnGetAsync(int id)
        {
            Reviews = await database.Review.Where(r => r.MovieID == id && r.UserID == accessControl.LoggedInUserID).ToListAsync();

            Movie = database.Movie.FirstOrDefault(m => m.ID == id);

            CreateEmptyReview();

        }
        public async Task<IActionResult> OnPostAsync(Review review, int id)
        {
            Reviews = await database.Review.Where(r => r.MovieID == id && r.UserID == accessControl.LoggedInUserID).ToListAsync();
            if (!ModelState.IsValid)
            {
                return Page();
            }
            else if (Reviews.Any())
            {
                ReviewID = Reviews
                    .Select(r => r.ID)
                    .SingleOrDefault();

                return RedirectToPage("./Edit", new { id = ReviewID });
            }
            CreateEmptyReview();
            Review.Text = review.Text;
            Review.Score = review.Score;
            Review.MovieID = id;

            database.Review.Add(Review);
            await database.SaveChangesAsync();
            return RedirectToPage("/Movies/Index");
        }
    }
}
