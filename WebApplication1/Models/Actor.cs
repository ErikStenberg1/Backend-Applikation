using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public Movie Movie { get; set; }
        [Required]
        public int MovieID { get; set; }
    }
}
