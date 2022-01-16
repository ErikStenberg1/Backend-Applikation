using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Data;
using WebApplication.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Pages.Actors
{
    public class CollaborationsModel : PageModel
    {
        private readonly ApplicationDbContext database;

        public CollaborationsModel(ApplicationDbContext database, AccessControl accessControl)
        {
            this.database = database;
        }
        public Actor Actor { get; set; }
        public List<Movie> Movies { get; set; }
        public string SortColumn { get; set; }
        public int SortColumn2 { get; set; }
        public List<SelectListItem> FirstActorList { get; set; }
        public List<SelectListItem> SecondActorList { get; set; }
        public int FirstActorID { get; set; }
        public int SecondActorID { get; set; }



        private async Task Setup()
        {
            FirstActorList = await database.Actor.AsNoTracking()
                .OrderBy(a => a.FirstName)
                .ThenBy(a => a.LastName)
                .Select(a => new SelectListItem
                {
                    Value = a.ID.ToString(),
                    Text = a.fullname
                })
                .ToListAsync();
            SecondActorList = await database.Actor.AsNoTracking()
                .OrderBy(a => a.FirstName)
                .ThenBy(a => a.LastName)
                .Select(a => new SelectListItem
                {
                    Value = a.ID.ToString(),
                    Text = a.fullname
                })
                .ToListAsync();

        }
        public async Task OnGetAsync()
        {
            await Setup();
        }
        public async Task OnPostAsync(Actor actor, Actor actor1)
        {
            await Setup();
            actor = await database.Actor.FindAsync(actor.ID);
            actor1 = await database.Actor.FindAsync(actor1.ID);

            var movies = await database.Movie
                .Where(m => m.Actors.Contains(actor))
                .ToListAsync();
        }
    }
}
