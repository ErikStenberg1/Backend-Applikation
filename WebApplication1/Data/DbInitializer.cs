//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;
//using WebApplication.Models;

//namespace WebApplication1.Data
//{
//    public class DbInitializer
//    {
//        public static void Initialize(ApplicationDbContext database)
//        {
//            if (database.Movie.Any())
//            {
//                return;
//            }
//            string[] lines = File.ReadAllLines(@"Data\movies.csv");
//            foreach (var line in lines)
//            {
//                string[] parts = line.Split(',');
//                Movie movie = new Movie
//                {
//                    Title = parts[0],
//                    Category = parts[1],
//                    ReleaseYear = int.Parse(parts[2])
//                };
//                database.Movie.Add(movie);
//            }
//            database.SaveChanges();
//            database.Movie.Add(new Movie
//            {
//                Title = "abc",
//                Category = "action",
//                ReleaseYear = 2000
//            });
//            database.SaveChanges()

//        }
//    }
//}
