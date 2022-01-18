using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication.Models;
using WebApplication1.Data;

namespace WebApplication1.Pages.Reviews
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext database;
        private readonly AccessControl accessControl;
        public EditModel (ApplicationDbContext database, AccessControl accessControl)
        {
            this.database = database;
            this.accessControl = accessControl;
        }
        public Review Review { get; set; }
        private async Task LoadReview(int id)
        {
            Review = await database.Review
                .Where(r => r.ID == id)
                .SingleOrDefaultAsync();
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            await LoadReview(id);
            var userReview = await database.Review
            .Where(r => r.ID == id)
            .ToListAsync();
            if (!userReview.Any())
            {
                return RedirectToPage("/Movies/Index");
            }
            else if (!accessControl.UserCanAccess(Review))
            {
                return Forbid();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id, Review review)
        {
            await LoadReview(id);
            var userReview = await database.Review
                .Where(r => r.ID == id)
                .ToListAsync();
            if (!userReview.Any())
            {
                return RedirectToPage("/Movies/Index");
            }
            else if (!accessControl.UserCanAccess(Review))
            {
                return Forbid();
            }

            Review.Text = review.Text;
            Review.Score = review.Score;

            await database.SaveChangesAsync();
            return RedirectToPage("/Movies/Index");
        }
    }
}
