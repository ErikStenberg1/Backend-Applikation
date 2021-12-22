using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Movie
    {
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public Category Category { get; set; }
        [Required]
        public int CategoryID { get; set; }
        public List<Actor> ActorList { get; set; }
        public List<Review> ReviewList { get; set; }
    }
}
