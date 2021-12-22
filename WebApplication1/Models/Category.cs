using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Category
    {
        public int ID { get; set; }

        public enum MovieCategory
        {
            Action,
            Comedy,
            Drama,
            Fantasy,
            Horror,
            Mystery,
            Romance,
            Thriller,
            Western
        }
    }
}
