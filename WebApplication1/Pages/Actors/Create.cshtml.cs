using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication.Models;
using WebApplication1.Data;

namespace WebApplication1.Pages.Actors
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext database;

        public CreateModel(ApplicationDbContext database)
        {
            this.database = database;
        }
        public Actor Actor { get; set; }
        private void CreateEmptyMovie()
        {
            Actor = new Actor
            {

            };
        }
        public async Task<IActionResult> OnPostAsync(Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            CreateEmptyMovie();
            Actor.FirstName = actor.FirstName;
            Actor.LastName = actor.LastName;
            Actor.Age = actor.Age;
            Actor.PosterPath = actor.PosterPath;

            database.Actor.Add(Actor);
            await database.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
