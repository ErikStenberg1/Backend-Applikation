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
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext database;
        private readonly AccessControl accessControl;

        public DeleteModel(ApplicationDbContext database, AccessControl accessControl)
        {
            this.database = database;
            this.accessControl = accessControl;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var review = await database.Review.FindAsync(id);
            if (!database.Review.Contains(review))
            {
                return RedirectToPage("./Index", new { id });
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var review = await database.Review.FindAsync(id);

            database.Review.Remove(review);
            await database.SaveChangesAsync();
            return RedirectToPage("./Index", new { id });
        }
    }
}
