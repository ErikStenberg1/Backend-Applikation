using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Data;
using WebApplication.Models;


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
        public int MovieID { get; set; }
        private void CreateEmptyReview()
        {
            Review = new Review
            {
                UserID = accessControl.LoggedInUserID
            };
        }
        public void OnGet(int id)
        {
            CreateEmptyReview();
        }
        public async Task<IActionResult> OnPostAsync(Review review)
        {
            CreateEmptyReview();

            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            Review.Text = review.Text;
            Review.Score = review.Score;
            Review.MovieID = 2;

            await database.Review.AddAsync(review);
            await database.SaveChangesAsync();
            return RedirectToPage("./Details", new { id = review.ID });
        }
    }
}
