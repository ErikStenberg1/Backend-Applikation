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
        public SelectList SortColumnList { get; set; }
        public SelectList SortColumnList2 { get; set; }
        public List<SelectListItem> Actors { get; set; }



        private async Task Setup()
        {
            Actors = await database.Actor.AsNoTracking()
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
            var actor2 = await database.Actor.FindAsync(actor.ID);
            var actor3 = await database.Actor.FindAsync(actor1.ID);
        }
    }
}
