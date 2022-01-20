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
        private readonly AccessControl accessControl;

        public CollaborationsModel(ApplicationDbContext database, AccessControl accessControl)
        {
            this.database = database;
            this.accessControl = accessControl;
        }
        public List<Movie> Movies { get; set; }
        public List<SelectListItem> ActorList { get; set; }
        [FromQuery]
        public int FirstActorID { get; set; }
        [FromQuery]
        public int SecondActorID { get; set; }



        private async Task Setup()
        {
            ActorList = await database.Actor.AsNoTracking()
                .OrderBy(a => a.FirstName)
                .ThenBy(a => a.LastName)
                .Select(a => new SelectListItem
                {
                    Value = a.ID.ToString(),
                    Text = a.FullName
                })
                .ToListAsync();
        }
        public async Task OnGetAsync()
        {
            await Setup();
            var firstActor = await database.Actor.FindAsync(FirstActorID);
            var secondActor = await database.Actor.FindAsync(SecondActorID);

            Movies = await database.Movie
                .Where(m => m.Actors.Contains(firstActor) && m.Actors.Contains(secondActor))
                .ToListAsync();

        }
    }
}
