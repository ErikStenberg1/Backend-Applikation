using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Actor
    {
        public int ID { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }
        public Int16 Age { get; set; }
        public string PosterPath { get; set; }
        public List<Movie> Movies { get; set; }
        [NotMapped]
        public string FullName { get { return this.FirstName + " " + this.LastName; } }
    }
}
