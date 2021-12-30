using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication1.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext database)
        {
            var erik = new Actor() { FirstName = "Erik", LastName = "Stenberg", Age = 19 };
            var avengers = new Movie() { Title = "Avengers", PosterPath = "brad.jpg", Category = "Action", ReleaseYear = 2020, Actors = new() { erik } };
            database.AddRange(erik, avengers);
            database.SaveChanges();
        }
    }
}
