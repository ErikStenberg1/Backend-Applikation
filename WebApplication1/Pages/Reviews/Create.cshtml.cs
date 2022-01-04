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
        public int movieID { get; set; }
        public List<Review> Count { get; set; }
        public IEnumerable<int> id { get; set; }
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
            Count = await database.Review.Where(r => r.MovieID == movieID && r.UserID == accessControl.LoggedInUserID).ToListAsync();
            id = Count.Select(review => review.ID);
            CreateEmptyReview(movieID);
        }
        public async Task<IActionResult> OnPostAsync(Review review, int movieID)
        {
            Count = await database.Review.Where(r => r.MovieID == movieID && r.UserID == accessControl.LoggedInUserID).ToListAsync();
            if (!ModelState.IsValid)
            {
                return Page();
            }
            else if (Count.Any())
            {
                id = Count.Select(review => review.ID);
                return RedirectToPage("./Edit", new { id });
            }
            CreateEmptyReview(movieID);

            Review.Text = review.Text;
            Review.Score = review.Score;


            await database.Review.AddAsync(review);
            await database.SaveChangesAsync();
            return RedirectToPage("./Details", new { id = review.ID });
        }
    }
}
