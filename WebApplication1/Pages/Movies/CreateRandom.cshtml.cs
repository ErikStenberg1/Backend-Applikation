using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;

namespace WebApplication1.Pages.Movies
{
    public class CreateRandomModel : PageModel
    {
        private readonly ApplicationDbContext database;

        public CreateRandomModel(ApplicationDbContext database)
        {
            this.database = database;
        }

        private string[] movies;
        private string[] actors;

        public async Task<IActionResult> OnPostAsync()
        {

            movies = await LoadColumnCSV(@"Data\movies.csv");
            foreach (var item in movies)
            {
                string[] parts = item.Split(',');
                var movie = new Movie
                {
                    Title = parts[0],
                    Category = parts[1],
                    ReleaseYear = int.Parse(parts[2])
                };
                database.Movie.Add(movie);
            }
            actors = await LoadColumnCSV(@"Data\actors.csv");
            foreach (var item in actors)
            {
                string[] parts = item.Split(',');
                var actor = new Actor
                {
                    FirstName = parts[0],
                    LastName = parts[1],
                    Age = Int16.Parse(parts[2]),
                };
                database.Actor.Add(actor);
            }
            await database.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
        //public async Task<IActionResult> OnPostAsync()
        //{
        //    actors = await LoadColumnCSV(@"Data\actors.csv");
        //    foreach (var item in actors)
        //    {
        //        string[] parts = item.Split(',');
        //        var actor = new Actor
        //        {
        //            FirstName = parts[0],
        //            LastName = parts[1],
        //            Age = Int16.Parse(parts[2]),
        //            MovieID = int.Parse(parts[3])
        //        };
        //        database.Actor.Add(actor);
        //    }
        //    await database.SaveChangesAsync();
        //    return RedirectToPage("./Index");
        //}

        public async Task<string[]> LoadColumnCSV(string path)
        {
            string[] lines = await System.IO.File.ReadAllLinesAsync(path);
            List<string> parts = new List<string> { };
                foreach (var line in lines.ToList())
                {
                    parts.Add(line);
                }
            return parts.ToArray();
        }
    }
    
}
