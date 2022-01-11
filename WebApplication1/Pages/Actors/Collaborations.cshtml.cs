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
        public List<Actor> Actors { get; set; }
        public List<Movie> Movies { get; set; }
        public string SortColumn { get; set; }
        public int SortColumn2 { get; set; }
        public SelectList SortColumnList { get; set; }
        public SelectList SortColumnList2 { get; set; }



        public async Task OnGetAsync()
        {
            SortColumnList = new SelectList(database.Actor, nameof(Actor.FirstName), nameof(Actor.fullname));
            SortColumnList2 = new SelectList(database.Actor, nameof(Actor.ID), nameof(Actor.fullname));

            var query = database.Movie.AsNoTracking()
                .Include(m => m.Actors)
                .Where(m => m.Actors.Any(a => a.fullname.ToLower().Contains(SortColumn.ToLower())));

            //Movies = await query.ToListAsync();
        }
    }
}
