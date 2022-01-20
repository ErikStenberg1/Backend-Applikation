using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication.Models;
using WebApplication1.Data;

namespace WebApplication1.Pages.Actors
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext database;

        public EditModel(ApplicationDbContext database)
        {
            this.database = database;
        }
        public Actor Actor { get; set; }
        public async Task LoadActor(int id)
        {
            Actor = await database.Actor
                .Where(a => a.ID == id)
                .SingleOrDefaultAsync();
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            await LoadActor(id);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id, Actor actor)
        {
            await LoadActor(id);
            Actor.FirstName = actor.FirstName;
            Actor.LastName = actor.LastName;
            Actor.Age = actor.Age;
            Actor.PosterPath = actor.PosterPath;

            await database.SaveChangesAsync();
            return RedirectToPage("./Index");

        }
    }
}
