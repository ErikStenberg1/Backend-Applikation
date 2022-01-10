using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Data;
using WebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Pages.Reviews
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext database;
        private readonly AccessControl accessControl;

        public DeleteModel(ApplicationDbContext database, AccessControl accessControl)
        {
            this.database = database;
            this.accessControl = accessControl;
        }
        public Movie Movie { get; set; }
        public int MovieID { get; set; }
        public Review Review { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Review = await database.Review
            .Where(r => r.ID == id)
            .SingleOrDefaultAsync();

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
        public async Task<IActionResult> OnPostAsync(int id)
        {
            Review = await database.Review
            .Where(r => r.ID == id)
            .SingleOrDefaultAsync();
            if (!accessControl.UserCanAccess(Review))
            {
                return Forbid();
            }

            var review = await database.Review.FindAsync(id);

            database.Review.Remove(review);
            await database.SaveChangesAsync();
            return RedirectToPage("/Movies/Index");
        }
    }
}
