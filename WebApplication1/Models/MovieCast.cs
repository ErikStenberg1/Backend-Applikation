using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication1.Models
{
    public class MovieCast
    {
        public int ID { get; set; }
        public Actor Actor { get; set; }
        public Movie Movie { get; set; }
    }
}
