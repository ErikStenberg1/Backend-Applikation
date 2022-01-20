using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Data;
using WebApplication.Models;
using Microsoft.EntityFrameworkCore;


namespace WebApplication1.Pages.Actors
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext database;

        public DeleteModel(ApplicationDbContext database)
        {
            this.database = database;
        }
        public Actor Actor { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Actor = await database.Actor
                .Where(a => a.ID == id)
                .SingleOrDefaultAsync();

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id)
        {
            Actor = await database.Actor
                .Where(a => a.ID == id)
                .SingleOrDefaultAsync();

            var actor = await database.Actor.FindAsync(id);

            database.Actor.Remove(actor);
            await database.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
