using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication.Models
{
    public class Movie
    {
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Category { get; set; }
        public int ReleaseYear { get; set; }
        public List<Review> ReviewList { get; set; }
        [Required]
        public string PosterPath { get; set; }
        public List<MovieCast> MovieCasts { get; set; }
    }
}
