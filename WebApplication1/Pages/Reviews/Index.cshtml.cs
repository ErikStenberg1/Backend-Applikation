using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication.Models;
using WebApplication1.Data;

namespace WebApplication1.Pages.Reviews
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext database;
        private readonly AccessControl accessControl;

        public IndexModel(ApplicationDbContext database, AccessControl accessControl)
        {
            this.database = database;
            this.accessControl = accessControl;
        }

        public List<Review> Reviews{ get; set; }
        public Review Review { get; set; }
        public Movie Movie { get; set; }
        public int movieID { get; set; }
        public async Task OnGetAsync(int movieID)
        {
            Reviews  = await database.Review
                .Where(r => r.MovieID == movieID)
                .Include(r => r.User)
                .OrderBy(r => r.Score)
                .ToListAsync();

        }
    }
}
