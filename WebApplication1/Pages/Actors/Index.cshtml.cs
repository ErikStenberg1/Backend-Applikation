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
        public const string name = "Name";
        public const string release = "Age";
        private string[] searchColumns = { actor, movie };
        [FromQuery]
        public string SearchColumn { get; set; }
        public SelectList SearchColumnList { get; set; }
        private string[] sortColums = { name, release};
        [FromQuery]
        public string SortColumn { get; set; }
        public SelectList SortColumnList { get; set; }


        public async Task OnGetAsync()
        {
            SearchColumnList = new SelectList(searchColumns, actor);
            SortColumnList = new SelectList(sortColums, release);

            var query = database.Actor.AsNoTracking();
            if (SearchTerm != null)
            {
                query = query.Where(a =>
                a.FirstName.ToLower().Contains(SearchTerm.ToLower()) ||
                a.LastName.ToLower().Contains(SearchTerm.ToLower()));
            }
            if (SearchColumn != null)
            {    
                if (SearchColumn == movie)
                {
                    query = query
                    .Include(actor => actor.Movies)
                    .Where(actor => actor.Movies.Any(movie => movie.Title.ToLower().Contains(SearchTerm.ToLower())));
                }
            }
            if (SortColumn != null)
            {
                if (SortColumn == name)
                {
                    query = query
                        .OrderBy(a => a.FirstName)
                        .ThenBy(a => a.LastName);
                }
                else if (SortColumn == release)
                {
                    query = query
                        .OrderBy(a => a.Age);
                }
            }
            Actors = await query.ToListAsync();
        }
    }
}
