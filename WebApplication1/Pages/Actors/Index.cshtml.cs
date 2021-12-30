using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication.Models;
using WebApplication1.Data;

namespace WebApplication1.Pages.Actors
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext database;
        public IndexModel(ApplicationDbContext database)
        {
            this.database = database;
        }

        public IList<Actor> Actors { get; set; }
        [FromQuery]
        public string SearchTerm { get; set; }
        public const string actor = "Actor";
        public const string movie = "Movie";
        private string[] sortColumns = { actor, movie };
        [FromQuery]
        public string SortColumn { get; set; }
        public SelectList SortColumnList { get; set; }
         
        public async Task OnGetAsync()
        {
            SortColumnList = new SelectList(sortColumns, actor);

            var query = database.Actor.AsNoTracking();
            if (SearchTerm != null)
            {
                query = query.Where(a =>
                a.FirstName.ToLower().Contains(SearchTerm.ToLower()) ||
                a.LastName.ToLower().Contains(SearchTerm.ToLower())).OrderBy(c => c.FirstName);
            }
            if (SortColumn != null)
            {    
                if (SortColumn == movie)
                {
                    query = database.Actor.AsNoTracking()
                    .Include(actor => actor.Movies)
                    .Where(actor => actor.Movies.Any(movie => movie.Title.ToLower().Contains(SearchTerm.ToLower())));
                }
            }
            Actors = await query.ToListAsync();
        }
    }
}
